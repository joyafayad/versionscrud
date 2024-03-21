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
            version.Projectid = req.projectId;
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
                    ProjectID = v.Projectid,
                    VersionNumber = v.Versionnumber
                })
                .ToListAsync();

            return Ok(versions);
        }
        [HttpPut]
        public async Task<ActionResult<VersionGetResp>> Update(int id, VersionUpdateReq req)
        {
            if (Id != req.Id)
            {
                return BadRequest();
            }

            var version = await _context.Versions.FindAsync(Id);

            if (version == null)
            {
                return NotFound();
            }

            version.Projectid = req.projectId;
            version.Versionnumber = req.versionNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!VersionExists(id))
            {
                return NotFound();
            }

            var response = new VersionGetResp
            {
                ID = version.Id,
                ProjectID = version.Projectid,
                VersionNumber = version.Versionnumber
            };

            return response;
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
