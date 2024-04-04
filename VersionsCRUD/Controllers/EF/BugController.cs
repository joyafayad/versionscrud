using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Bug;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers.EF
{
  
        [ApiController]
        [Route("[controller]")]
        public class BugController : ControllerBase
        {
            private readonly postgresContext _context;

            public BugController(postgresContext context)
            {
                _context = context;
            }

        // POST: /bug/add
        [HttpPost]
        public async Task<ActionResult<BugAddResp>> AddBug(BugAddReq req)
        {
           
            if (req == null)
            {
                return BadRequest("Invalid request");
            }

            // Convert the Release string to a DateTime object
            if (!DateTime.TryParse(req.Release, out DateTime releaseDate))
            {
                var errorResponse = new BugAddResp
                {
                    code = 5,
                    Message = "Invalid release date format"
                };
                return Ok(errorResponse);
            }

            try
            {
                var bug = new Bug
                {
                    Id = Guid.NewGuid(),
                    Description = req.Description,
                    Status = req.Status,
                    release = releaseDate.ToString(), 
                   
                };

                _context.Bugs.Add(bug);
                await _context.SaveChangesAsync();

                var resp = new BugAddResp
                {
                    Id = bug.Id,
                    code = 0 
                };

                return Ok(resp); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the bug."); 
            }
        }

    }

    
}
