using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Models;
using VersionsCRUD.User;

namespace VersionsCRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly postgresContext _context;

        public UserController(postgresContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usersDb = await _context.Users.ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VersionsCRUD.Models.User, UserGet>();
            });
            var mapper = config.CreateMapper();

            // Map the users to DTOs
            var usersDto = mapper.Map<List<UserGet>>(usersDb);

            // Create the response object
            var resp = new UserGetResp
            {
                users = usersDto,
                totalCount = usersDto.Count
            };

            return View(resp);
        }
    }
}
