using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using VersionsCRUD.Feature;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers.EF
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class FeatureController : ControllerBase
    {
        private readonly postgresContext _context;

        public FeatureController(postgresContext context)
        {
            _context = context;
        }

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

            //plz ma77e hode w tab2e mtl reported taba3 l bug

            var feature = new VersionsCRUD.Models.Feature();
            feature.Id = Guid.NewGuid(); // Generate a new GUID for the project
            feature.Name = req.name;
            feature.Description = req.description;
            //feature.Release = (DateOnly?)req.release; //check

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            resp.id = feature.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

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

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

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

        //[HttpPost]
        //public async Task<ActionResult<LoadDataResponse>> LoadData(object _logger)
        //{
        //    try
        //    {
        //        var features = await _context.Features
        //       .Select(f => new FeatureLoadDataResponse { Id = f.Id, name = f.Name })
        //       .ToListAsync();


        //        var response = new LoadDataResponse
        //        {
        //            Features = features,

        //        };
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while loading data.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while loading data.");
        //    }
        //}

    }
}