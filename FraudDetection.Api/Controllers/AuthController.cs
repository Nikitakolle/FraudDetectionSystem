using FraudDetection.Api.Data;
using FraudDetection.Api.DTOs;
using Microsoft.AspNetCore.Identity;
using FraudDetection.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FraudDetection.Api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthController(IConfiguration configuration, ApplicationDbContext context, 
            IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto request)
        {
            var user = _context.Users.FirstOrDefault(
            u => u.Username == request.Username);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var result =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (result ==
                PasswordVerificationResult.Failed)
            {
                return Unauthorized(
                    "Invalid username or password");
            }



            // ================= CREATE CLAIMS =================

            var claims = new[]
            {
               new Claim(
               ClaimTypes.Name,
               user.Username),

               new Claim(
               ClaimTypes.NameIdentifier,
               user.Id.ToString())
            };

            // ================= CREATE KEY =================

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]));

            // ================= CREATE TOKEN =================

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims: claims,

                    expires:
                        DateTime.Now.AddHours(2),

                    signingCredentials:
                        credentials
                );

            // ================= RETURN TOKEN =================

            return Ok(new
            {
                token =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token)
            });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDto request)
        {
            var existingUser =
                _context.Users.FirstOrDefault(
                    u => u.Username == request.Username);

            if (existingUser != null)
            {
                return BadRequest(
                    "Username already exists");
            }

            var user = new User
            {
                Username = request.Username,

                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.Password);

            _context.Users.Add(user);

            _context.SaveChanges();

            return Ok(
                "User registered successfully");
        }
    }
}