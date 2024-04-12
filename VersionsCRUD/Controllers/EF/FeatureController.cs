﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Feature;
using NLog;
using VersionsCRUD.Models;

namespace VersionsCRUD
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class FeatureController : ControllerBase
    {
        private readonly postgresContext _context;
        private object releaseDate;
        private object _feature;
        private readonly ILogger<FeatureController> _logger;



        public FeatureController(postgresContext context)
        {

            _context = context;
        }

        private static Logger GetLogger(Logger logger)
        {
            return logger;
        }

        // POST: /feature/add
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

            // Convert the Release string to a DateTime object
            if (!DateTime.TryParse(req.Release, out DateTime Release))
            {

                var errorResponse = new FeatureAddResp
                {
                    code = 5,
                    message = "Invalid release date format"
                };
                return Ok(errorResponse);
            }

            var feature = new Feature
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                //Release = (DateOnly?)releaseDate, //discuss about it
                Created = DateTime.UtcNow,
                Isactive = true
            };

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();


            //var resp = new FeatureAddResp
            //{
            //    Id = feature.Id,
            //    code = 0 // Assuming success
            //};

            return Ok(resp);
        }

        // POST: /feature/get
        [HttpPost]
        public async Task<ActionResult<IEnumerable<FeatureGetResp>>> Get(FeatureGetByIdReq req)
        {
            //var features = await _context.Features.ToListAsync();

            //if (features == null)
            //{
            //    return NotFound();
            //}

            //return Ok(features.Select(f => new FeatureGetResp
            //{
            //    Id = f.Id,
            //    Name = f.Name,
            //    Description = f.Description,
            //    // Release = f.Release?.ToUniversalTime() //check it
            //}));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var features = await _context.Features
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();


            return Ok(features);
        }


        // POST: /feature/update
        [HttpPost]
        public async Task<ActionResult<FeaturesUpdateResp>> Update(FeaturesUpdateReq req)
        {
            var feature = await _context.Features.FindAsync(req.Id);

            if (feature == null)
            {
                return NotFound();
            }

            feature.Name = req.Name;

            try
            {
                _context.Features.Update(feature);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var resp = new FeaturesUpdateResp
            {
                code = 0
            };

            return Ok(resp);
        }


        // POST: /feature/getbyid
        [HttpPost]
        public async Task<ActionResult<FeatureGetResp>> GetById(FeatureGetByIdReq req)
        {

            var feature = await _context.Features.FindAsync(req.Id);


            if (feature == null)
            {
                return NotFound();
            }


            var featureResp = new FeatureGetResp
            {
                Id = feature.Id,
                Name = feature.Name,
                Description = feature.Description,
                // Release = feature.Release,

            };

            return Ok(featureResp);
        }


        [HttpPost]//check 
        public async Task<IActionResult> Delete(FeatureDeleteReq req)
        {

            var feature = await _context.Features.FindAsync(req.Id);


            if (feature == null)
            {
                return NotFound();
            }


            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return NoContent();
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





