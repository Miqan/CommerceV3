using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceV3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommerceV3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager= userManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }
        public async Task<IActionResult>Delete(string id)
        {
            var users = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(users);
            return RedirectToAction("Index");
        }
    }
}