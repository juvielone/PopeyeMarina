using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PopeyeMarina.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("/Login")]
        public IActionResult Login()
        {
            return View();
        }


        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Login(string username, string password)
        //{
        //    var adminUsername = _configuration["Admin:Username"];
        //    var passwordHash = _configuration["Admin:PasswordHash"];

        //    if (username == adminUsername &&
        //        !string.IsNullOrEmpty(passwordHash) &&
        //        BCrypt.Net.BCrypt.Verify(password, passwordHash))
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, username)
        //        };

        //        var identity = new ClaimsIdentity(
        //            claims,
        //            CookieAuthenticationDefaults.AuthenticationScheme);

        //        var principal = new ClaimsPrincipal(identity);

        //        await HttpContext.SignInAsync(
        //            CookieAuthenticationDefaults.AuthenticationScheme,
        //            principal);

        //        return RedirectToAction("Index", "Customers");
        //    }

        //    ViewBag.Error = "Invalid username or password.";
        //    return View();
        //}


        [AllowAnonymous]
        [HttpPost("/Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var adminUsername = _configuration["Admin:Username"];
            var passwordHash = _configuration["Admin:PasswordHash"];

            Console.WriteLine("=== LOGIN DEBUG ===");
            Console.WriteLine($"Entered username: '{username}'");
            Console.WriteLine($"Config username: '{adminUsername}'");
            Console.WriteLine($"Password hash exists: {!string.IsNullOrEmpty(passwordHash)}");
            Console.WriteLine($"Password hash length: {passwordHash?.Length}");

            bool usernameMatches = username == adminUsername;

            Console.WriteLine($"Username matches: {usernameMatches}");

            bool passwordMatches = false;

            if (!string.IsNullOrEmpty(passwordHash))
            {
                passwordMatches = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }

            Console.WriteLine($"Password matches: {passwordMatches}");

            if (usernameMatches && passwordMatches)
            {
                Console.WriteLine("LOGIN SUCCESS - creating cookie");

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                Console.WriteLine("COOKIE CREATED");

                return RedirectToAction("Index", "Customers");
            }

            Console.WriteLine("LOGIN FAILED");

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}