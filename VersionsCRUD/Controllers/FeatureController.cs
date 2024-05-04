using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Feature;
using VersionsCRUD.Models;

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
    }
}
