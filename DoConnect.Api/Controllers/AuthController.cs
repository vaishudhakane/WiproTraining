using DoConnect_Api.Data;
using DoConnect.Api.Dtos;
using DoConnect.Api.Entities;
using DoConnect.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoConnect.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
                return BadRequest(new { message = "Username already exists" });   //changes made

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                // Role = dto.Role
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role  //changes done
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // return Ok(new { Message = "User registered successfully" }); 
            return CreatedAtAction(nameof(Login), new { id = user.Id }, new { message = "Registered successfully" });  //changes made
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.Id, user.Username, user.Role);
            return Ok(new
            { Token = token ,
              role=user.Role,
              userId=user.Id,                //changes made for role,userId,username after getting bugs
              username=user.Username
            });
        }
    }
}
