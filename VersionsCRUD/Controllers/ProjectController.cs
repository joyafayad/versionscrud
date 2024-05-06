using System.Net.NetworkInformation;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Mapping;
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


        /// <summary>
        /// add a new project
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProjectAddResp>> Add(ProjectAddReq req)
        {
            ProjectAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            //To Handle project already added same name

            var project = new VersionsCRUD.Models.Project();
            project.Id = Guid.NewGuid(); // Generate a new GUID for the project
            project.Name = req.name;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            resp.id = project.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// get a list of project
        /// </summary>
        /// <param name="req"></param>
        ///  <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProjectGetResp> Get(ProjectGetReq req)
        {
            ProjectGetResp resp = new();

            List<VersionsCRUD.Models.Project> projectsDb = await _context.Projects

          .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProject>();
            });
            var mapper = config.CreateMapper();

            // Map the projects to DTOs
            resp.projects = mapper.Map<List<VersionsCRUD.Models.Project>, List<ProjectGet>>(projectsDb);
            resp.totalCount = resp.projects.Count;


            resp.code = 0;
            resp.message = "Success";
            return resp;

        }

        /// <summary>
        /// update a project
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProjectUpdateResp>> Update(ProjectUpdateReq req)
        {
            ProjectUpdateResp resp = new();
            var project = await _context.Projects.FindAsync(req.id);

            if (project == null)
            {
                //Handled project not found
                resp.code = 10;
                resp.message = "Project Not Found";
                return resp;
            }

            project.Id = req.id;
            project.Name = req.name;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// getbyid a project
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProjectGetByIdResp>> GetById(ProjectGetByIdReq req)
        {
            ProjectGetByIdResp resp = new();
            VersionsCRUD.Models.Project project = await _context.Projects.FindAsync(req.id);

            if (project == null)
            {
                //Handled project not found
                resp.code = 10;
                resp.message = "Project Not Found";
                return resp;
            }

            resp.project = new ProjectGet
            {
                id = project.Id,
                name = project.Name,
            };

            resp.code = 0;
            resp.message = "Success";

            return resp;
        }
        /// <summary>
        /// delete a project
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
		public async Task<ActionResult<ProjectDeleteResp>> Delete(ProjectDeleteReq req)
		{
			ProjectDeleteResp resp = new();

			var project = await _context.Projects.FindAsync(req.id);

			if (project == null)
			{
				//Handled project not found
				resp.code = 10;
				resp.message = "Project Not Found";
				return resp;
			}

			_context.Projects.Remove(project);

			//var environments = _context.Environments.Where();
			//_context.Environments.Remove(enviroments);

			await _context.SaveChangesAsync();

			resp.code = 0;
			resp.message = "Success";
			return resp;
		}
	}
	//ViewData["username"] = User.FindFirstValue("test");
	//return View();
}
	

	

