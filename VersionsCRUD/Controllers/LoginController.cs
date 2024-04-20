using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VersionsCRUD.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data;
using System.Security.Claims;

namespace VersionsCRUD.Controllers
{
	//[Authorize]
	public class LoginController : Controller
	{
		//private readonly SignInManager<IdentityUser> _signInManager;

		//public LoginController(SignInManager<IdentityUser> signInManager)
		//{
		//	_signInManager = signInManager;
		//}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> LoginAsync(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.username == "test" && model.password == "test")
				{
					var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
					identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.username));
					identity.AddClaim(new Claim(ClaimTypes.Name, model.username));
					identity.AddClaim(new Claim("test", model.username));
					var principal = new ClaimsPrincipal(identity);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				}
			}

			return RedirectToAction("Index", "Home");
		}
	}
}
