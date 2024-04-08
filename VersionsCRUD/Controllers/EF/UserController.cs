﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.User;
using VersionsCRUD.Models;

namespace VersionsCRUD.Controllers.EF
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly postgresContext _context;

        public UserController(postgresContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UserAddResp>> Add(UserAddReq req)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = req.Username,
                    Email = req.Email,
                    Password = req.Password,
                    // Created = DateTime.UtcNow,
                    Isactive = true
                };


                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                var resp = new UserAddResp
                {
                    Id = user.Id,
                    code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the user: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserGetResp>>> Get(UserGetByIdReq req)
        {
            //try
            //{
            //    var users = await _context.Users
            //        .Select(u => new UserGetResp
            //        {
            //            Id = u.Id,
            //            Username = u.Username,
            //            Email = u.Email,
            //            // Created = u.Created,
            //            //IsActive = u.Isactive
            //        })
            //        .ToListAsync();

                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users
           .Skip((req.pagenumber - 1) * req.pagesize)
           .Take(req.pagesize)
           .ToListAsync();

            return Ok(users);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "An error occurred while retrieving users: " + ex.Message);
            //}
        }

        [HttpPost]
        public async Task<ActionResult<UserUpdateResp>> Update(UserUpdateReq req)
        {
            try
            {
                var user = await _context.Users.FindAsync(req.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Username = req.Username;
                user.Email = req.Email;
                user.Password = req.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                var resp = new UserUpdateResp
                {
                    Code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDeleteResp>> Delete(UserDeleteReq req)
        {
            try
            {
                var user = await _context.Users.FindAsync(req.Id);

                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                var resp = new UserDeleteResp
                {
                    Code = 0
                };

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the user: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserGetResp>> GetUserById(UserGetByIdReq req)
        {
            try
            {
                var user = await _context.Users.FindAsync(req.Id);

                if (user == null)
                {
                    return NotFound();
                }

                var userGetResp = new UserGetResp
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,

                };

                return Ok(userGetResp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the user: " + ex.Message);
            }
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


        private string GenerateToken(User user)
        {

            return "dummy_token";
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenRequest request)
        {

            var newToken = "dummy_refreshed_token";


            return Ok(new { token = newToken });
        }

        [HttpPost]
        public IActionResult Logout(TokenRequest request)
        {

            var user = _context.Users.FirstOrDefault(u => u.Token == request.Token);

            if (user != null)
            {

                user.Isloggedin = false;
               // user.Lastlogout = DateTime.UtcNow;
                _context.SaveChanges();

                return Ok(new { message = "User logged out successfully" });
            }


            return Unauthorized(new { message = "Invalid token" });
        }
    }
}








