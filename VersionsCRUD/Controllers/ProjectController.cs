using System.Net.NetworkInformation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using VersionsCRUD.Views.Project;
	
namespace VersionsCRUD.Controllers
{
	[Authorize]
	public class ProjectController : Controller
	{
		public IActionResult Index()
		{
			var model = new TableDataViewModel
			{
				TableData = new List<string[]>
				{
					new string[] {"Tiger Nixon", "System Architect", "Edinburgh", "5421", "2011/04/25", "$3,120",},
					new string[] {"Garrett Winters", "Director", "Edinburgh", "8422", "2011/07/25", "$5,300"}
				}
			};
			ViewData["TableData"] = model;
			return View(model);
			//ViewData["username"] = User.FindFirstValue("test");
			//return View();
		}
	}
	
}
