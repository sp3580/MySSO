using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSOTEST.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public class AccountInfo
        {
            public string account { get; set; }
            public string pwd { get; set; }
        }

        [HttpPost]
        public ActionResult Login([FromBody] AccountInfo info)
        {
            if (info.account != null && info.pwd != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, info.account),
                    // new Claim("FullName", info.pwd),
                     new Claim(ClaimTypes.Role, "Administrator")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Json("");
            }
            else
            {
                return Json("登入失敗");
            }

        }

        //[HttpDelete]
        public async Task<ViewResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return await Task.Run(() => View("Index"));
        }

    }
}
