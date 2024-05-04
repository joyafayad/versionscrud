using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Models;
using VersionsCRUD.Version;

namespace VersionsCRUD.Controllers
{
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
    }
}
