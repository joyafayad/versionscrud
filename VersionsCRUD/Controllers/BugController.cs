using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Bug;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers
{
    public class BugController : Controller
    {
        private readonly postgresContext _context;

        public BugController(postgresContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bugsDb = await _context.Bugs.ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionsCRUD.Models.Bug, BugGet>();
            });
            var mapper = config.CreateMapper();

            // Map the bugs to DTOs
            var bugsDto = mapper.Map<List<BugGet>>(bugsDb);

            // Create the response object
            var resp = new BugGetResp
            {
                bugs = bugsDto,
                totalCount = bugsDto.Count
            };

            return View(resp);
        }
    }
}
