using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;
using VersionsCRUD.Version;

namespace VersionsCRUD.Controllers.EF
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

        [HttpPost]
        public async Task<ActionResult<VersionAddResp>> Add(VersionAddReq req)
        {
            VersionAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            //To Handle project doesn't exist

            var version = new VersionsCRUD.Models.Version();
            version.Id = Guid.NewGuid(); // Generate a new GUID for the project
            version.Projectid = req.projectId;
            version.Versionnumber = req.versionNumber;

            _context.Versions.Add(version);
            await _context.SaveChangesAsync();

            resp.id = version.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<VersionGetResp>> Get(VersionGetReq req)
        {
            VersionGetResp resp = new();

            List<VersionsCRUD.Models.Version> versionsDb = await _context.Versions
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingVersion>();
            });
            var mapper = config.CreateMapper();

            // Map the projects to DTOs
            resp.versions = mapper.Map<List<VersionsCRUD.Models.Version>, List<VersionGet>>(versionsDb);

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<VersionUpdateResp>> Update(VersionUpdateReq req)
        {
            VersionUpdateResp resp = new();
            var version = await _context.Versions.FindAsync(req.id);

            if (version == null)
            {
                //Handled version not found
                resp.code = 15;
                resp.message = "Version Not Found";
                return resp;
            }

            version.Projectid = req.projectId;
            version.Versionnumber = req.versionNumber;

            _context.Versions.Update(version);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<VersionDeleteResp>> Delete(VersionDeleteReq req)
        {
            VersionDeleteResp resp = new();
            var version = await _context.Versions.FindAsync(req.id);

            if (version == null)
            {
                //Handled version not found
                resp.code = 15;
                resp.message = "Version Not Found";
                return resp;
            }

            _context.Versions.Remove(version);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<VersionGetByIdResp>> GetById(VersionGetByIdReq req)
        {
            VersionGetByIdResp resp = new();
            var version = await _context.Versions.FindAsync(req.id);

            if (version == null)
            {
                //Handled version not found
                resp.code = 15;
                resp.message = "Version Not Found";
                return resp;
            }

            resp.version = new VersionGet
            {
                id = version.Id,
                projectId = version.Projectid,
                versionNumber = version.Versionnumber,
            };

            resp.code = 0;
            resp.message = "Success";

            return resp;
        }

    }
}
