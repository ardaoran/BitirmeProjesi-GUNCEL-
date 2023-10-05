using BitirmeProjesi.DAL.Entities;
using BitirmeProjesi.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesi.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager; 
        }

        
        public async Task <IActionResult> Index(UserSignInViewModel p )
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password,false,true);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("kullanici", p.username);
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
           
            return View();
        }
    }
}
