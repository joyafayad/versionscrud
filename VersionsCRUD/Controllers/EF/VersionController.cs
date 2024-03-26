using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        private readonly postgresContext _context;
        private Guid Id;
        private ActionResult<VersionUpdateResp> resp;
        



        public VersionController(postgresContext context)
        {
            _context = context;
        }

        // POST: api/Versions
        [HttpPost]
        public async Task<ActionResult<VersionAddResp>> Add(VersionAddReq req)
        {
            VersionAddResp resp = new();

            if (req == null)
            {
                return BadRequest();
            }

            var version = new VersionsCRUD.Models.Version();
            version.ProjectId = req.projectId;
            version.Versionnumber = req.versionNumber;

            _context.Versions.Add(version);
            await _context.SaveChangesAsync();

            //return CreatedAtRoute("GetVersion", new { id = version.Id }, version);
            resp.idVersion = version.Id;
            resp.code = 0;
            return resp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersionGetResp>>> Get()
        {
            var versions = await _context.Versions
                .Select(v => new VersionGetResp
                {
                    ID = v.Id,
                    ProjectID = v.ProjectId,
                    VersionNumber = v.Versionnumber
                })
                .ToListAsync();

            return Ok(versions);
        }

        [HttpPut]
        public async Task<ActionResult<VersionUpdateResp>> Update(int id, VersionUpdateReq req)
        {
            if (Id != req.Id)
            {
                return BadRequest();
            }

            var version = await _context.Versions.FindAsync(req.Id);

            if (version == null)
            {
                return NotFound("Version not found.");
            }

            version.Id= req.Id;
            version.ProjectId = req.projectId; 
            version.Versionnumber = req.versionNumber; 

            try
            {
                _context.Versions.Update(version);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {



                if (!VersionExists(Guid id))
                {
                    return NotFound("Version not found.");
                }
               


            }

            // Construct response with return code
            var resp = new VersionUpdateResp
            {
                code = 0
            };
            return resp;


        }

        private bool VersionExists(int id)
        {
            return _context.Versions.Any(e => e.Id == Id);
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var version = await _context.Versions.FindAsync(id);
            if (version == null)
            {
                return NotFound();
            }

            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
