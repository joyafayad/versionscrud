using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Bug;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers.EF
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BugController : ControllerBase
    {
        private readonly postgresContext _context;

        public BugController(postgresContext context)
        {
            _context = context;
        }

        static bool IsValidDateFormat(string input)
        {
            // Here you can implement your own logic for validating date format
            // This example assumes the date format is YYYY-MM-DD
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d{4}-\d{2}-\d{2}$");
        }

        /// <summary>
        /// Add a new bug
        /// </summary>
        /// <param name="req">request</param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BugAddResp>> Add(BugAddReq req)
        {
            BugAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            DateOnly dateReported;
            if (string.IsNullOrEmpty(req.reported))
            {
                //If Reported Date is not provided --> Fill by today's date
                req.reported = DateTime.Today.ToString("yyyy-MM-dd");
            }
            //To validate that format is yyyy-MM-dd
            if (!DateOnly.TryParse(req.reported, out dateReported))
            {
                //We are converting the string to DateOnly by this method
                resp.code = 6;
                resp.message = "Invalid reported date format";
                return resp;
            }

            var bug = new VersionsCRUD.Models.Bug();
            bug.Id = Guid.NewGuid(); // Generate a new GUID for the project
            bug.Description = req.description;
            bug.Status = req.status;
            bug.Reported = dateReported;

            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();

            resp.id = bug.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<BugUpdateResp>> Update(BugUpdateReq req)
        {
            BugUpdateResp resp = new();
            var bug = await _context.Bugs.FindAsync(req.id);

            if (bug == null)
            {
                //Handled bug not found
                resp.code = 12;
                resp.message = "Bug Not Found";
                return resp;
            }

            bug.Description = req.description;
            bug.Status = req.status;

            _context.Bugs.Update(bug);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<BugDeleteResp>> Delete(BugDeleteReq req)
        {
            BugDeleteResp resp = new();
            var bug = await _context.Bugs.FindAsync(req.id);

            if (bug == null)
            {
                //Handled bug not found
                resp.code = 12;
                resp.message = "Bug Not Found";
                return resp;
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<BugGetResp>> Get(BugGetReq req)
        {
            BugGetResp resp = new();

            List<VersionsCRUD.Models.Bug> bugsDb = await _context.Bugs
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingBug>();
            });
            var mapper = config.CreateMapper();

            // Map the projects to DTOs
            resp.bugs = mapper.Map<List<VersionsCRUD.Models.Bug>, List<BugGet>>(bugsDb);

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        [HttpPost]
        public async Task<ActionResult<BugGetByIdResp>> GetById(BugGetByIdReq req)
        {
            BugGetByIdResp resp = new();
            var bug = await _context.Bugs.FindAsync(req.id);

            if (bug == null)
            {
                //Handled bug not found
                resp.code = 12;
                resp.message = "Bug Not Found";
                return resp;
            }

            resp.bug = new BugGet
            {
                id = bug.Id,
                description = bug.Description,
                status = bug.Status.Value,
                reported = bug.Reported.ToString(), //Convert
            };

            resp.code = 0;
            resp.message = "Success";

            return resp;
        }

        //[HttpPost]
        //public async Task<ActionResult<LoadDataResponse>> LoadData()
        //{
        //    try
        //    {
        //        var bugs = await _context.Bugs
        //       .Select(b => new BugLoadDataResponse{ Id = b.Id, description = b.Description })
        //       .ToListAsync();

        //        var response = new LoadDataResponse
        //        {
        //            Bugs = bugs
        //        };

        //        return Ok(response);
        //    }
        //       catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while loading data.");
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while loading data.");
        //    }
        //}

        //public void PrintBugStatus() // fkre aamel class jdide
        //{
        //    var globalData = new Globaldata();



        //    var bugStatusEnglish = globalData.GetGlobalDataByTypeAndLanguage("bugstatus", 1);
        //    Console.WriteLine("Bug Status in English:");
        //    foreach (var data in bugStatusEnglish)
        //    {
        //        //Console.WriteLine($"{data.name}: {data.Value}");
        //    }

        //    var bugStatusFrench = globalData.GetGlobalDataByTypeAndLanguage("bugstatus", 2);
        //    Console.WriteLine("\nBug Status in French:");
        //    foreach (var data in bugStatusFrench)
        //    {
        //        //  Console.WriteLine($"{data.Name}: {data.Value}");
        //    }
        //}
    
    }
}