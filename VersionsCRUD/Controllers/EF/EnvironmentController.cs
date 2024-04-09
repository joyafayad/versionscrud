using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Environment;
using VersionsCRUD.Common;
using VersionsCRUD.Models;
using Environment = VersionsCRUD.Models.Environment;


namespace VersionsCRUD
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EnvironmentController : ControllerBase
    {
        private readonly postgresContext _context;

        public EnvironmentController(postgresContext context)
        {
            _context = context;
        }


        // POST: /environment/add
        [HttpPost]
        public async Task<ActionResult<EnvironmentAddResp>> Add(EnvironmentAddReq req)
        {
            if (req == null)
            {
                return BadRequest("Invalid request");
            }

            var environment = new Environment
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description
                
            };

            _context.Environments.Add(environment);
            await _context.SaveChangesAsync();

            var resp = new EnvironmentAddResp
            {
                environmentid = environment.Id,
                code = 0 
            };

            return Ok(resp);
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<EnvironmentGetResp>>> Get(EnvironmentGetByIdReq req)
        {
            try
            {

                var environments = await _context.Environments
                    .Select(e => new EnvironmentGetResp
                    {
                        Id = e.Id,
                        projectname = e.Name,
                        description = e.Description,

                    })
                    .ToListAsync();


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var paginatedEnvironments = await _context.Environments
                    .Skip((req.pagenumber - 1) * req.pagesize)
                    .Take(req.pagesize)
                    .ToListAsync();

                return Ok(paginatedEnvironments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving environments: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EnvironmentUpdateResp>> Update(EnvironmentUpdateReq req)
        {
            try
            {
                var environment = await _context.Environments.FindAsync(req.Id);

                if (environment == null)
                {
                    return NotFound();
                }

                environment.Name = req.Name ?? environment.Name;
                environment.Description = req.Description ?? environment.Description;
                environment.Projectid = req.projectid;

                _context.Environments.Update(environment);
                await _context.SaveChangesAsync();

                var resp = new EnvironmentUpdateResp
                {
                    code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating environment: " + ex.Message);
            }
        }
        [HttpPost]

        public async Task<ActionResult<EnvironmentDeleteResp>> Delete(EnvironmentDeleteReq req)
        {
            try
            {
                var environment = await _context.Environments.FindAsync(req.Id);

                if (environment == null)
                {
                    return NotFound();
                }

                _context.Environments.Remove(environment);
                await _context.SaveChangesAsync();

                var resp = new EnvironmentDeleteResp
                {
                    code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting environment: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<EnvironmentGetResp>> GetById(EnvironmentGetByIdReq req)
        {
            try
            {
                var environment = await _context.Environments.FindAsync(req.Id);

                if (environment == null)
                {
                    return NotFound("Environment not found");
                }

                var environmentResponse = new EnvironmentGetResp
                {
                    Id = environment.Id,
                    //Name = environment.Name,
                    //Description = environment.Description,
                    //IsActive = environment.IsActive,
                    //CreatedDate = environment.CreatedDate,
                    //CreatedBy = environment.CreatedBy,
                    //UpdatedDate = environment.UpdatedDate,
                    //UpdatedBy = environment.UpdatedBy
                };

                return Ok(environmentResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving environment: {ex.Message}");
            }
        }






    }
}


