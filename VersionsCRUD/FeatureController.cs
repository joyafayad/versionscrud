using Microsoft.AspNetCore.Mvc;
using test.Models;
using VersionsCRUD.Models;

namespace VersionsCRUD
{
   
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private readonly postgresContext _context;
        private object releaseDate;

        public FeatureController(postgresContext context)
        {
            _context = context;
        }

        //post//api //feature
        [HttpPost]
        public async Task<ActionResult<FeatureAddResp>> AddFeature(FeauterAddReq req)
        {
            
            if (req == null)
            {
                return BadRequest("Invalid request");
            }

            // Convert the Release string to a DateTime object
            if (!DateTime.TryParse(req.Release, out DateTime Release))
            {
                
                var errorResponse = new FeatureAddResp
                {
                    code = 5,
                    Message = "Invalid release date format"
                };
                return Ok (errorResponse);  
            }

            var feature = new Feature
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                Release = (DateOnly?)releaseDate,
                Created = DateTime.UtcNow,
                Isactive = true
            };

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

           
            var resp = new FeatureAddResp
            {
                Id = feature.Id,
                code = 0 // Assuming success
            };

            return Ok(resp); 
        }

    }



}
