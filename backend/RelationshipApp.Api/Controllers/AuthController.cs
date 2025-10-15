using Microsoft.AspNetCore.Mvc;
using RelationshipApp.Api.DTOs;
using RelationshipApp.Services.Interfaces;

namespace RelationshipApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var (user, token, refreshToken) = await _authService.RegisterAsync(
                request.Email,
                request.Password,
                request.DisplayName
            );

            if (user == null)
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            var response = new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token!,
                RefreshToken = refreshToken!
            };

            return CreatedAtAction(nameof(Register), new { id = user.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user registration");
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var (user, token, refreshToken) = await _authService.LoginAsync(
                request.Email,
                request.Password
            );

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var response = new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token!,
                RefreshToken = refreshToken!
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var (user, token) = await _authService.RefreshTokenAsync(request.RefreshToken);

            if (user == null || token == null)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            var response = new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token,
                RefreshToken = request.RefreshToken
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token refresh");
            return StatusCode(500, new { message = "An error occurred during token refresh" });
        }
    }
}
