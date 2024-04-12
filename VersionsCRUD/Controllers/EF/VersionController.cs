﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Versions;
using test.Models;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VersionController : ControllerBase
    {

        private readonly postgresContext _context;
        //private Guid Id;
        private ActionResult<VersionUpdateResp> resp;




        public VersionController(postgresContext context)
        {
            _context = context;
        }

        // POST: api/Versions
        [HttpPost]
        public async Task<ActionResult<FeatutreAddResp>> Add(VersionAddReq req)
        {
            FeatutreAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<VersionGetResp>>> Get(VersionGetByIdReq req)
        {
            //var versions = await _context.Versions
            //    .Select(v => new VersionGetResp
            //    {
            //        ID = v.Id,
            //        ProjectID = v.Projectid,
            //        VersionNumber = v.Versionnumber
            //    })
            //    .ToListAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var versions = await _context.Versions
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            return Ok(versions);


        }


        [HttpPost]
        public async Task<ActionResult<VersionUpdateResp>> Update(VersionUpdateReq req)
        {
            var version = await _context.Versions.FindAsync(req.Id);

            if (version == null)
            {
                return NotFound("Version not found.");
            }

            version.Id = req.Id;
            version.Projectid = req.projectId;
            version.Versionnumber = req.versionNumber;

            try
            {
                _context.Versions.Update(version);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {



                //if (!VersionExists(Guid id))
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

        private bool VersionExists(Guid id)
        {
            return _context.Versions.Any(e => e.Id == id);
        }


        [HttpPost]
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

        [HttpPost]
        public async Task<ActionResult<VersionGetResp>> GetById(VersionGetByIdReq req)
        {
            var version = await _context.Versions.FindAsync(req.Id);

            if (version == null)
            {
                return NotFound();
            }

            return Ok(version);
        }




    }
}
