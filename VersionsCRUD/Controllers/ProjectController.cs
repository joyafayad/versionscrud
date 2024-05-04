using System.Net.NetworkInformation;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Models;
using VersionsCRUD.Project;
//using VersionsCRUD.Views.Project;



namespace VersionsCRUD.Controllers
{
	[Authorize]
	public class ProjectController : Controller
	{

		private readonly postgresContext _context;


		public ProjectController(postgresContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{

			var projectsDb = await _context.Projects.ToListAsync();

			// Configure AutoMapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<VersionsCRUD.Models.Project, ProjectGet>();
			});
			var mapper = config.CreateMapper();

			// Map the projects to DTOs
			var projectsDto = mapper.Map<List<ProjectGet>>(projectsDb);

			// Create the response object
			var resp = new ProjectGetResp
			{
				projects = projectsDto,
				totalCount = projectsDto.Count
			};

			return View(resp);
		}
	}
	//ViewData["username"] = User.FindFirstValue("test");
	//return View();
}
	

	

