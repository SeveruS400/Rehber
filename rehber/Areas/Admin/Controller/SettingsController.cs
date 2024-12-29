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
        private readonly IServiceManager _serviceManager;

        public SettingsController(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IServiceManager serviceManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int sessionTimeout)
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
                        return RedirectToAction("Index"); // Geri yönlendirme
                    }
                    else
                    {
                        // Hata durumunda hata mesajı gösterilir
                        TempData["ErrorMessage"] = "There was an error updating the session timeout.";
                    }
                }
            }

            return RedirectToAction("Index"); // Eğer model geçerli değilse formu tekrar render ederiz
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View(new UserDtoForCreation()
        //    {
        //        Roles = new HashSet<string>(_serviceManager
        //            .AuthService
        //            .Roles
        //            .Select(r => r.Name)
        //            .ToList())
        //    });
        //}
        [HttpGet]
        public IActionResult Create()
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

            return View(new UserDtoForCreation()
            {
                Roles = availableRoles
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result = await _serviceManager.AuthService.CreateUser(userDto);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();

        }
    }
}