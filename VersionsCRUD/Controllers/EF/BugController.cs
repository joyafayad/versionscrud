using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Bug;
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

        // POST: /bug/add
        [HttpPost]
        public async Task<ActionResult<BugAddResp>> Add(BugAddReq req)
        {

            if (req == null)
            {
                return BadRequest("Invalid request");
            }

            // Convert the Release string to a DateTime object
            //if (!DateTime.TryParse(req.Release, out DateTime releaseDate))
            //{
            //    var errorResponse = new BugAddResp
            //    {
            //        code = 5,
            //        Message = "Invalid release date format"
            //    };
            //    return Ok(errorResponse);
            //}


            var bug = new Bug
            {
                Id = Guid.NewGuid(),
                Description = req.Description,
                Status = req.Status,
                //release = releaseDate.ToString(), 

            };

            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();

            var resp = new BugAddResp
            {
                Id = bug.Id,
                code = 0
            };

            return Ok(resp);




        }



        [HttpPost]
        public async Task<ActionResult<BugUpdateResp>> Update(BugUpdateReq req)
        {
            try
            {
                var bug = await _context.Bugs.FindAsync(req.Id);

                if (bug == null)
                {
                    return NotFound();
                }

                bug.Description = req.Description;
                bug.Status = req.Status;

                _context.Bugs.Update(bug);
                await _context.SaveChangesAsync();

                //if (DateTime.TryParse(req.Fixed, out DateTime fixedDate))
                //{
                //    bug.Fixed = fixedDate;
                //}
                //else
                //{
                //    // Handle invalid date format
                //    var errorResponse = new BugUpdateResp
                //    {
                //        code = 5, 
                //    };
                //    return BadRequest(errorResponse);
                //}


                var resp = new BugUpdateResp
                {
                    code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating bug: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BugDeleteResp>> Delete(BugDeleteReq req)
        {
            try
            {
                var bug = await _context.Bugs.FindAsync(req.Id);

                if (bug == null)
                {
                    return NotFound();
                }

                _context.Bugs.Remove(bug);
                await _context.SaveChangesAsync();

                var resp = new BugDeleteResp
                {
                    code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting bug: " + ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<BugGetResp>>> Get(BugGetByIdReq req)
        {
            try
            {
                //{
                //    var bugs = await _context.Bugs
                //        .Select(b => new BugGetResp
                //        {
                //            Id = b.Id,
                //            Description = b.Description,
                //            //Reported = bug.Reported,
                //            //Fixed = bug.Fixed,
                //            //Status = bug.Status



                //        })
                //        .ToListAsync();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var bugs = await _context.Bugs
                    .Skip((req.pagenumber - 1) * req.pagesize)
                    .Take(req.pagesize)
                    .ToListAsync();

                return Ok(bugs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving bugs: " + ex.Message);
            }
        }

       
               [HttpPost] 
                public async Task<ActionResult<BugGetResp>> GetById(BugGetByIdReq req)
                {
                   try
                   {

                     var bug = await _context.Bugs.FindAsync(req.Id);

                      if (bug == null)
                      {
                               return NotFound("Bug not found");
                      }


                          var bugResponse = new BugGetResp
                          {
                                Id = bug.Id,
                                Description = bug.Description,
                               //Reported = bug.Reported,
                              //Fixed = bug.Fixed,
                               //Status = bug.Status

                          };


                          return Ok(bugResponse);
                      }
                       catch (Exception ex)
                    {

                        return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bug: {ex.Message}");
                   }
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

        public void PrintBugStatus()
        {
            var globalData = new Globaldata();
       
            

            var bugStatusEnglish = globalData.GetGlobalDataByTypeAndLanguage("bugstatus", 1);
            Console.WriteLine("Bug Status in English:");
            foreach (var data in bugStatusEnglish)
            {
                //Console.WriteLine($"{data.name}: {data.Value}");
            }

            var bugStatusFrench = globalData.GetGlobalDataByTypeAndLanguage("bugstatus", 2);
            Console.WriteLine("\nBug Status in French:");
            foreach (var data in bugStatusFrench)
            {
              //  Console.WriteLine($"{data.Name}: {data.Value}");
            }
        }
    }










}













