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

        public FeatureController(postgresContext context)
        {
            _context = context;
        }

        // POST: api/feature
        [HttpPost]
        public async Task<ActionResult<FeatureAddResp>> AddFeature(FeauterAddReq req)
        {
            FeatutreAddResp resp = new();
            if (req == null)
            {
                return BadRequest("Invalid request");
            }

            DateTime? release = req.Release;
            var feature = new Feature
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
             
            };

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();
            _ = new FeatureAddResp
            {
                Id = feature.Id,
                code = 0 // Assuming success
            };

            return Ok(resp); 
        }
    }


}
