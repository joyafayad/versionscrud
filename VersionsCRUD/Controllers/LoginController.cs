using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using VersionsCRUD.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data;
using System.Security.Claims;
using VersionsCRUD.User;

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
				#region Dynamic Login
				UserController c = new UserController(new postgresContext());
				var result = await c.ListUsers();

				var userList = result.users.ToList();

				foreach (var user in userList)
				{
					if (user != null && user.username == model.username && user.password == model.password)
					{
						var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
						identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.username));
						identity.AddClaim(new Claim(ClaimTypes.Name, model.username));
						var principal = new ClaimsPrincipal(identity);

						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
					}
				}
				#endregion


				#region Static Login
				//if (model.username == "test" && model.password == "test")
				//{
				//	var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				//	identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.username));
				//	identity.AddClaim(new Claim(ClaimTypes.Name, model.username));
				//	var principal = new ClaimsPrincipal(identity);

				//	await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
				//}
				#endregion
			}

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			//HttpContext.Session.Clear();

			//SignOutAsync is Extension method for SignOut 
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies"); 
			
			return RedirectToAction("Index") ;
		}
	}
}
