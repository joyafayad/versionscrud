using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Bug;
using VersionsCRUD.Environment;
using VersionsCRUD.Feature;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;
using VersionsCRUD.Project;
using VersionsCRUD.Version;

namespace VersionsCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VersionController : Controller
    {
        private readonly postgresContext _context;

        public VersionController(postgresContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var versionsDb = await _context.Versions.ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionsCRUD.Models.Version, VersionGet>();
            });
            var mapper = config.CreateMapper();

            // Map the versions to DTOs
            var versionsDto = mapper.Map<List<VersionGet>>(versionsDb);

            // Create the response object
            var resp = new VersionGetResp
            {
                versions = versionsDto,
                totalCount = versionsDto.Count
            };

            return View(resp);
        }


        /// <summary>
        /// add a version
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
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

        /// <summary>
        /// get a list of version
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
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
            resp.totalCount = resp.versions.Count;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// update a version
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
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

        /// <summary>
        /// delete a version
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
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

        /// <summary>
        /// getbyid a list of version
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
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

        [HttpGet]
        public async Task<ActionResult<VersionLoadDataResp>> LoadData()
        {
            VersionLoadDataResp resp = new();
            resp.projects = await _context.Projects
                .Select(b => new ProjectResp { id = b.Id, name = b.Name })
                .ToListAsync();
            resp.environments = await _context.Environments
                .Select(b => new EnvironmentResp { id = b.Id, name = b.Name })
                .ToListAsync();
            resp.features = await _context.Features
                .Select(b => new FeatureResp { id = b.Id, name = b.Name })
                .ToListAsync();
            resp.bugs = await _context.Bugs
                .Select(b => new BugResp { id = b.Id, name = b.Description })
                .ToListAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    }
}

