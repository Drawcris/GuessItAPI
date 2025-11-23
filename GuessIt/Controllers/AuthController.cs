using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GuessIt.Services;
using GuessIt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using GuessIt.DTOs;
using GuessIt.Responses;

namespace GuessIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }

            var token = _authService.GenerateJwtTokenAsync(dto.Email);
            return Ok(new { token.Result });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordDTO dto)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userEmail == null)
            {
                return Unauthorized("User email not found in token.");
            }

            var result = await _authService.ChangePasswordAsync(dto, userEmail);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not found in token.");
            }

            var profile = await _authService.MyProfileAsync(userId);

            var result = new ApiResponse<Dictionary<string, string?>>(
                success: true, 
                message: "Profile fetched successfully", 
                data: profile);
            
            return Ok(result);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
