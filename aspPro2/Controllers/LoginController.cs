using aspPro2.Migrations;
using aspPro2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;
using System.Security;
using System.Security.Claims;

namespace aspPro2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult signUp()
        {
            return View();
        }

        [HttpPost, ActionName("Sign In")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> SignInCONFIRM(string email, String password)
        {
            CookieOptions cookieop = new CookieOptions();
            cookieop.Expires = DateTimeOffset.UtcNow.AddDays(1);


            Response.Cookies.Append("UserPass", password);
            Response.Cookies.Append("UserEmail", email);
            if (email != null)
            {
                var user = await _context.users
                    .FirstOrDefaultAsync(m => m.Email == email && m.password == password);

                if (user != null)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("sign up")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> signUpForm(Users person, String Email, string password)
        {
            CookieOptions cookieop = new CookieOptions();
            cookieop.Expires = DateTimeOffset.UtcNow.AddDays(1);


            Response.Cookies.Append("UserPass", password);
            Response.Cookies.Append("UserEmail", Email);

            if (Email != null)
            {
                _context.users.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", "login", new { id = person.Id, email = Email });
            }
            return NotFound();
        }



        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var pass = Request.Cookies["UserPass"];
            var email = Request.Cookies["UserEmail"];

            if (email != null && pass != null)
            {
                var user = await _context.users
                    .FirstOrDefaultAsync(x => x.Email == email && x.password == pass);
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return RedirectToAction(nameof(signUp));
            }
        }







        [HttpPost]
        
        public async Task<IActionResult> Profile(Users users)
        {
            var pass = Request.Cookies["UserPass"];
            var email = Request.Cookies["UserEmail"];

            
            if (email != null && pass != null)
            {
                var user = await _context.users
                    .FirstOrDefaultAsync(x => x.Email == email && x.password == pass);
                if (user != null)
                {
                    user.Name = users.Name;
                    user.LastName = users.LastName;
                    user.Email = users.Email;
                    user.password = users.password;



                    _context.users.Update(user);
                    await _context.SaveChangesAsync();
                    return View(user);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }



        public IActionResult logout()
        {
			Response.Cookies.Delete("UserPass");
			Response.Cookies.Delete("UserEmail");


			return RedirectToAction(nameof(signUp));
        }
    }
}

