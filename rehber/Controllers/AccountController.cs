using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rehber.Models;

namespace rehber.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await _userManager.FindByNameAsync(model.Name);
                if (user is not null)
                {
                    await _signInManager.SignOutAsync(); // Önceki oturumları kapat

                    var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        // Kullanıcının rolünü al
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            // Eğer kullanıcı admin ise Admin Area'ya yönlendir
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }
                        else if (roles.Contains("Editor") && !roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Editor" });
                        }
                        else
                        {
                            // Diğer kullanıcılar için ana sayfaya yönlendirme
                            return Redirect(model?.ReturnUrl ?? "/");
                        }
                    }
                }

                ModelState.AddModelError("Error", "Invalid username or password");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name ="ReturnUrl")] string ReturnUrl="/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }

    }
}
