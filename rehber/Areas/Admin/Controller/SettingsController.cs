using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace rehber.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]

    public class SettingsController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IServiceManager _serviceManager;

        public SettingsController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IServiceManager serviceManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceManager = serviceManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int sessionTimeout, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı almak için mevcut IdentityUser'ı kullanıyoruz
                Users user = await _userManager.GetUserAsync(User); // Mevcut oturumdaki kullanıcıyı alırız

                if (user != null)
                {
                    // User'ın sessionTimeout değerini güncelleme
                    user.SessionTimeoutInHours = sessionTimeout;

                    // Kullanıcıyı güncellemek için UserManager'ı kullanıyoruz
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Session timeout updated successfully!";
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        // Hata durumunda hata mesajı gösterilir
                        TempData["ErrorMessage"] = "Oturum Zamanı Güncellenemedi";
                        return Redirect(returnUrl);
                    }
                }
            }

            return Redirect(returnUrl); // Eğer model geçerli değilse formu tekrar render ederiz
        }

        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            // Kullanıcıyı oluşturan kullanıcının rolünü al
            var currentUserRoles = _serviceManager.AuthService.GetCurrentUserRoles(User.Identity.Name);

            // Admin veya Editor rolüne sahip kullanıcılar için uygun rolleri belirle
            var availableRoles = new HashSet<string>();

            // Admin ve Editor kullanıcıları, admin, editor ve user rollerini görebilir
            if (currentUserRoles.Contains("Admin"))
            {
                availableRoles = new HashSet<string>(_serviceManager.AuthService.Roles.Select(r => r.Name));
            }
            // Editor kullanıcıları, sadece editor ve user rollerini görebilir
            else if (currentUserRoles.Contains("Editor"))
            {
                availableRoles.Add("Editor");
                availableRoles.Add("User");
            }
            TempData["returnUrl"] = returnUrl;
            return View(new UserDtoForCreation()
            {
                Roles = availableRoles
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto, string returnUrl)
        {
            var result = await _serviceManager.AuthService.CreateUser(userDto);
            if (result == null || !result.Succeeded)
            {
                TempData["Error"] = "Kullanıcı Oluşturulamadı. Kullanıcı adı/Mail kullanımda";
                return Redirect(returnUrl);
            }
            return Redirect(returnUrl);

        }

		//[HttpPost]
		//public IActionResult ResetConnStatus()
		//{
		//	_serviceManager.ProductService.ResetAllConnStatus(); // Servis metodu çağrılır
		//	TempData["Success"] = "Tüm ürünlerin bağlantı durumu sıfırlandı.";
		//	return RedirectToAction("Index","Product"); // Ayarlar sayfasına yönlendirin
		//}
        [HttpPost]
        public async Task<IActionResult> VerifyPasswordAndReset(string password, string returnUrl)
        {
            // Şu anki oturum açmış kullanıcıyı alın
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return Redirect(returnUrl);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Şifre hatalı. Bağlantı durumları sıfırlanmadı.";
                return Redirect(returnUrl);
            }

            // Eğer şifre doğruysa bağlantı durumlarını sıfırlayın
            _serviceManager.ProductService.ResetAllConnStatus();

            TempData["Success"] = "Tüm bağlantı durumları başarıyla sıfırlandı.";
            return Redirect(returnUrl);
        }
    }
}