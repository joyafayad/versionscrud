using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VersionsCRUD.Mapping;
using VersionsCRUD.Models;
using VersionsCRUD.User;

namespace VersionsCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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

        /// <summary>
        /// add a user
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserAddResp>> Add(UserAddReq req)
        {
            UserAddResp resp = new();

            if (req == null)
            {
                //Handled request null
                resp.code = 1;
                resp.message = "Something went wrong. Please try again later ! ";
                return resp;
            }

            var user = new VersionsCRUD.Models.User();
            user.Id = Guid.NewGuid(); // Generate a new GUID for the project
            user.Username = req.username;
            user.Email = req.email;
            user.Password = req.password;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            resp.id = user.Id;
            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// get a list of user
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserGetResp>> Get(UserGetReq req)
        {
            UserGetResp resp = new();

            List<VersionsCRUD.Models.User> usersDb = await _context.Users
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingUser>();
            });
            var mapper = config.CreateMapper();

            // Map the projects to DTOs
            resp.users = mapper.Map<List<VersionsCRUD.Models.User>, List<UserGet>>(usersDb);
            resp.totalCount = resp.users.Count;

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// update a user
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserUpdateResp>> Update(UserUpdateReq req)
        {
            UserUpdateResp resp = new();
            var user = await _context.Users.FindAsync(req.id);

            if (user == null)
            {
                //Handled user not found
                resp.code = 14;
                resp.message = "User Not Found";
                return resp;
            }

            user.Username = req.username;
            user.Email = req.email;
            user.Password = req.password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// delete a user
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDeleteResp>> Delete(UserDeleteReq req)
        {
            UserDeleteResp resp = new();
            var user = await _context.Users.FindAsync(req.id);

            if (user == null)
            {
                //Handled user not found
                resp.code = 14;
                resp.message = "User Not Found";
                return resp;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            resp.code = 0;
            resp.message = "Success";
            return resp;
        }

        /// <summary>
        /// getbyid a user
        /// </summary>
        /// <param name="req"></param>
        /// <remarks>
        /// codes : 0 - Success / 6- Invalid reported date format <br/>
        /// reported date format : yyyy-MM-dd
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserGetByIdResp>> GetById(UserGetByIdReq req)
        {
            UserGetByIdResp resp = new();
            var user = await _context.Users.FindAsync(req.id);

            if (user == null)
            {
                //Handled user not found
                resp.code = 14;
                resp.message = "User Not Found";
                return resp;
            }

            resp.user = new UserGet
            {
                id = user.Id,
                username = user.Username,
                email = user.Email,
                password = user.Password,
            };

            resp.code = 0;
            resp.message = "Success";

            return resp;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user != null && VerifyPassword(request.Password, user.Password))
            {

                user.Isloggedin = true;
                //user.Lastlogin = DateTime.UtcNow;
                _context.SaveChanges();
                var token = GenerateToken(user);

                return Ok(new { code = 0, token });
            }
            else
            {

                return Unauthorized(new { code = 1211212, message = "Invalid credentials" });
            }
        }


        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {

            return enteredPassword == storedPassword;
        }

        private string GenerateToken(VersionsCRUD.Models.User user)
        {

            return "dummy_token";
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenRequest request)
        {

            var newToken = "dummy_refreshed_token";


            return Ok(new { token = newToken });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Logout(TokenRequest request)
        //{

        //    var user = _context.Users.FirstOrDefault(u => u.Token == request.Token);

        //    if (user != null)
        //    {

        //        user.Isloggedin = false;
        //        // user.Lastlogout = DateTime.UtcNow;
        //        _context.SaveChanges();

        //        return Ok(new { message = "User logged out successfully" });
        //    }


        //    return Unauthorized(new { message = "Invalid token" });
        //}
    }
}

