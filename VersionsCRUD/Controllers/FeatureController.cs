using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Feature;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;
using VersionsCRUD.Project;

namespace VersionsCRUD.Controllers
{
    public class FeatureController : Controller
    {
        private readonly postgresContext _context;

        public FeatureController(postgresContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featuresDb = await _context.Features.ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionsCRUD.Models.Feature, FeatureGet>();
            });
            var mapper = config.CreateMapper();

            // Map the features to DTOs
            var featuresDto = mapper.Map<List<FeatureGet>>(featuresDb);

            // Create the response object
            var resp = new FeatureGetResp
            {
                features = featuresDto,
                totalCount = featuresDto.Count
            };

            return View(resp);
        }

        /// <summary>
        /// add a new feature
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FeatureAddResp>> Add(FeatureAddReq req)
        {
            FeatureAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            DateOnly dateRelease;
            if (string.IsNullOrEmpty(req.release))
            {
                //If Release Date is not provided --> Fill by today's date
                req.release = DateTime.Today.ToString("yyyy-MM-dd");
            }
            //To validate that format is yyyy-MM-dd
            if (!DateOnly.TryParse(req.release, out dateRelease))
            {
                //We are converting the string to DateOnly by this method
                resp.code = 6;
                resp.message = "Invalid reported date format";
                return resp;
            }

            var feature = new VersionsCRUD.Models.Feature();
            feature.Id = Guid.NewGuid(); // Generate a new GUID for the project
            feature.Name = req.name;
            feature.Description = req.description;
            feature.Release = dateRelease;

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            resp.id = feature.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// get a list of feature
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FeatureGetResp>> Get(FeatureGetReq req)
        {
            FeatureGetResp resp = new();

            List<VersionsCRUD.Models.Feature> featuresDb = await _context.Features
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingFeature>();
            });
            var mapper = config.CreateMapper();

            // Map the projects to DTOs
            resp.features = mapper.Map<List<VersionsCRUD.Models.Feature>, List<FeatureGet>>(featuresDb);
            resp.totalCount = resp.features.Count;

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// update a feature
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FeatureUpdateResp>> Update(FeaturesUpdateReq req)
        {
            FeatureUpdateResp resp = new();
            var feature = await _context.Features.FindAsync(req.id);

            if (feature == null)
            {
                //Handled feature not found
                resp.code = 13;
                resp.message = "Feature Not Found";
                return resp;
            }

            feature.Name = req.name;
            feature.Description = req.description;
            //feature.Release = req.release;

            _context.Features.Update(feature);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// getbyid a feature
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FeatureGetByIdResp>> GetById(FeatureGetByIdReq req)
        {
            FeatureGetByIdResp resp = new();
            var feature = await _context.Features.FindAsync(req.id);

            if (feature == null)
            {
                //Handled feature not found
                resp.code = 13;
                resp.message = "Feature Not Found";
                return resp;
            }

            resp.feature = new FeatureGet
            {
                id = feature.Id,
                name = feature.Name,
                description = feature.Description,
                //release = feature.Release,
            };

            resp.code = 0;
            resp.message = "Success";

            return resp;
        }

        /// <summary>
        /// delete a feature
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FeatureDeleteResp>> Delete(FeatureDeleteReq req)
        {
            FeatureDeleteResp resp = new();
            var feature = await _context.Features.FindAsync(req.id);

            if (feature == null)
            {
                //Handled feature not found
                resp.code = 13;
                resp.message = "Feature Not Found";
                return resp;
            }

            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<FeatureLoadDataResp>> LoadData()
        {
            FeatureLoadDataResp resp = new();
            resp.projects = await _context.Projects
                .Select(b => new ProjectResp { id = b.Id, name = b.Name })
                .ToListAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

    }
}

