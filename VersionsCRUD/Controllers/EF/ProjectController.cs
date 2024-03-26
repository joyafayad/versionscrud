using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
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
                return BadRequest();
            }

            var project = new VersionsCRUD.Models.Project(); 
            project.Id = Guid.NewGuid(); // Generate a new GUID for the project
            project.Name = req.Name;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            resp.id = project.Id;
            resp.code = 0;
            return resp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectGetResp>>> Get()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectGetResp
                {
                    ID = p.Id,
                    name = p.Name
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, ProjectUpdateReq req)
        {
            if (id != req.Id)
            {
                return BadRequest();
            }

            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }
            project.Id=req.Id;
            project.Name = req.Name;

            try
            {
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
