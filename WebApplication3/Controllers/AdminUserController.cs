using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication3.EFCore;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly EF_DataContext _context;

        public AdminUserController(IConfiguration config, EF_DataContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("usersWithToken")]
        [AllowAnonymous]
        public IActionResult GetUsersWithToken([FromBody] TokenRequestModel model)
        {
            // Extract token from the request body
            if (string.IsNullOrEmpty(model?.Token))
            {
                return BadRequest("JWT token is required.");
            }

            try
            {
                // Validate and decode the JWT token
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(model.Token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return BadRequest("Invalid JWT token.");
                }

                // Extract claims from the token
                var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (userRole == "Admin")
                {
                    // Retrieve users or perform other operations
                    var users = _context.Users
                        .Select(u => new
                        {
                            u.FirstName,
                            u.LastName,
                            Email = u.EmailAddress,
                            u.UserType
                        })
                        .ToList();

                    return Ok(users);
                }
                else
                {
                    return Unauthorized("User does not have permission to access this resource.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("unprotected")]
        public IActionResult GetUnprotectedData()
        {
            var data = new { message = "This data is accessible without authentication" };
            return Ok(data);
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.UserType) // Ensure role is included
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // Use UtcNow instead of Now
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Model to represent the token in the request body
    public class TokenRequestModel
    {
        public string Token { get; set; }
    }
}
