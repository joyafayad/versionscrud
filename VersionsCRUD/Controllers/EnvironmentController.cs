using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Environment;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers
{
	public class EnvironmentController : Controller
	{

		private readonly postgresContext _context;

		public EnvironmentController(postgresContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var environmentsDb = await _context.Environments.ToListAsync();

			// Configure AutoMapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<VersionsCRUD.Models.Environment, EnvironmentGet>();
			});
			var mapper = config.CreateMapper();

			// Map the environments to DTOs
			var environmentsDto = mapper.Map<List<EnvironmentGet>>(environmentsDb);

			// Create the response object
			var resp = new EnvironmentGetResp
			{
				environments = environmentsDto,
				totalCount = environmentsDto.Count
			};

			return View(resp);
		}
	}
}


