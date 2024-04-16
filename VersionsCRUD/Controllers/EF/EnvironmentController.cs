using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Environment;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers.EF
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

        /// <summary>
        /// add a new environment
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        // POST: /environment/add
        [HttpPost]
        public async Task<ActionResult<EnvironmentAddResp>> Add(EnvironmentAddReq req)
        {
            EnvironmentAddResp resp = new();
            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            //To Handle here project exist in table project msh mhandal

            var environment = new VersionsCRUD.Models.Environment
            {
                Id = Guid.NewGuid(),
                Name = req.name,
                Description = req.description,
                Projectid = req.projectid
            };

            _context.Environments.Add(environment);
            await _context.SaveChangesAsync();

            resp.id = environment.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// get a list of environment
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EnvironmentGetResp>> Get(EnvironmentGetReq req)
        {
            EnvironmentGetResp resp = new();

            if (!ModelState.IsValid)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            List<VersionsCRUD.Models.Environment> environmentsDb = await _context.Environments
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingEnvironment>();
            });
            var mapper = config.CreateMapper();

            // Map the environments to DTOs
            resp.environments = mapper.Map<List<VersionsCRUD.Models.Environment>, List<EnvironmentGet>>(environmentsDb);
            resp.totalCount = resp.environments.Count;


            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// update environment
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EnvironmentUpdateResp>> Update(EnvironmentUpdateReq req)
        {
            EnvironmentUpdateResp resp = new();

            var environment = await _context.Environments.FindAsync(req.id);

            if (environment == null)
            {
                //Handled environment not found
                resp.code = 11;
                resp.message = "Environment Not Found";
                return resp;
            }

            environment.Name = req.name ?? environment.Name;
            environment.Description = req.description ?? environment.Description;
            environment.Projectid = req.projectid;

            _context.Environments.Update(environment);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// delete an environemnet
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EnvironmentDeleteResp>> Delete(EnvironmentDeleteReq req)
        {
            EnvironmentDeleteResp resp = new();
                var environment = await _context.Environments.FindAsync(req.id);

                if (environment == null)
                {
                    //Handled environment not found
                    resp.code = 11;
                    resp.message = "Environment Not Found";
                    return resp;
                }

                _context.Environments.Remove(environment);
                await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// getbyid an environment
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EnvironmentGetByIdResp>> GetById(EnvironmentGetByIdReq req)
        {
            EnvironmentGetByIdResp resp = new();
            VersionsCRUD.Models.Environment environment = await _context.Environments.FindAsync(req.id);

            if (environment == null)
            {
                //Handled environment not found
                resp.code = 11;
                resp.message = "Environment Not Found";
                return resp;
            }

            resp.environment = new EnvironmentGet
            {
                id = environment.Id,
                name = environment.Name,
                description = environment.Description,
                projectid = environment.Projectid,
            };

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<EnvironmentLoadDataResp>> LoadData()
        {
            EnvironmentLoadDataResp resp = new();

            resp.projects = await _context.Projects
                    .Select(p => new ProjectResp { id = p.Id, name = p.Name })
                    .ToListAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

    }
}