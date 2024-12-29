using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace rehber.Areas.Editor.Controllers
{
    [Area("Editor")]
    [Authorize(Roles = "Editor")]
    public class DashboardController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public DashboardController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var usersWithRoles = await _serviceManager.AuthService.GetAllUsersByRole();
            return View(usersWithRoles);
        }

        public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
        {
            var user = await _serviceManager.AuthService.GetOneUserForUpdate(id);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
        {
            if (ModelState.IsValid)
            {
                await _serviceManager.AuthService.Update(userDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDto()
            {
                UserName = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                // Model geçerli değilse, hatalarla birlikte formu tekrar göster
                return View(model);
            }
            var result = await _serviceManager.AuthService.ResetPassword(model);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View(model);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string userName)
        {
            await _serviceManager.AuthService.DeleteOneUser(userName);
            return RedirectToAction("Index");
        }
    }
}
