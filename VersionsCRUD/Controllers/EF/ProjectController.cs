using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Versions;
using System.Linq;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;
using VersionsCRUD.Project;

namespace VersionsCRUD.Controllers.EF
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly postgresContext _context;

        public ProjectController(postgresContext context)
        {
            _context = context;
        }

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

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

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

            //try
            //{
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProjectExists(req.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

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
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        //[HttpPost]
        //public async Task<ActionResult<LoadDataResponse>> LoadData()
        //{
        //    try
        //    {
        //        var projects = await _context.Projects
        //            .Select(p => new ProjectLoadDataResponse { Id = p.Id, name = p.Name })
        //            .ToListAsync();
        //        var response = new LoadDataResponse
        //        {
        //            Projects = projects,
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while loading data.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while loading data.");
        //    }
        //}
    }
}