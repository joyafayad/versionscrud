using Microsoft.AspNetCore.Mvc;
using test.Models;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        private readonly postgresContext _context;

        public VersionController(postgresContext context)
        {
            _context = context;
        }

        // POST: api/Versions
        [HttpPost]
        public async Task<ActionResult<VersionAddResp>> CreateVersion(VersionAddReq req)
        {
            VersionAddResp resp = new();

            if (req == null)
            {
                return BadRequest();
            }

            var version = new VersionsCRUD.Models.Version();
            version.Projectid = req.projectId;
            version.Versionnumber = req.versionNumber;

            _context.Versions.Add(version);
            await _context.SaveChangesAsync();

            //return CreatedAtRoute("GetVersion", new { id = version.Id }, version);
            resp.idVersion = version.Id;
            resp.code = 0;
            return resp;
        }


    }
}
