using InsuranceApp.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InsuranceApp.Api.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InsuranceApp.Api.Controllers;

/// <summary>
/// Provides API endpoints for user authentication and registration.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;

    public AuthController(IAuthManager authManager) => _authManager = authManager;

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerDto">The registration data.</param>
    /// <returns>HTTP 200 if registration succeeds; otherwise, HTTP 400 with error details.</returns>
    /// <response code="200">User registered successfully.</response>
    /// <response code="400">Registration failed due to invalid data or existing user.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        IdentityResult result = await _authManager.RegisterAsync(registerDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="loginDTO">The login credentials.</param>
    /// <returns>A JWT token if login succeeds; otherwise, HTTP 401 Unauthorized.</returns>
    /// <response code="200">Login successful. Returns a JWT token.</response>
    /// <response code="401">Invalid email or password.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        JwtTokenDTO? tokenDTO = await _authManager.LoginAsync(loginDTO);
        if (tokenDTO is null)
        {
            return Unauthorized();
        }

        return Ok(tokenDTO);
    }

    /// <summary>
    /// Issues a new JWT and refresh token pair using a valid refresh token.
    /// </summary>
    /// <param name="refreshTokenDTO">The refresh token request.</param>
    /// <returns>A new JWT token and refresh token if the provided refresh token is valid; otherwise, HTTP 401 Unauthorized.</returns>
    /// <response code="200">Refresh successful. Returns a new JWT and refresh token pair.</response>
    /// <response code="401">The provided refresh token is invalid or expired.</response>
    [HttpPost("refresh")]
    public async Task<ActionResult<JwtTokenDTO?>> Refresh([FromBody] RefreshTokenDTO refreshTokenDTO)
    {
        JwtTokenDTO? jwtTokenDTO = await _authManager.RefreshTokenAsync(refreshTokenDTO);
        if (jwtTokenDTO is null)
        {
            return Unauthorized();
        }

        return Ok(jwtTokenDTO);
    }

    /// <summary>
    /// Logs out the currently authenticated user by deleting their refresh token.
    /// </summary>
    /// <remarks>
    /// Requires a valid JWT token in the Authorization header.  
    /// After logout, the user must log in again to obtain new tokens.
    /// </remarks>
    /// <returns>
    /// The deleted refresh token if one existed; otherwise, HTTP 404 if no refresh token was found for the user.
    /// </returns>
    /// <response code="200">Logout successful.</response>
    /// <response code="401">The request is unauthorized due to a missing or invalid JWT.</response>
    /// <response code="404">No refresh token was found for the authenticated user.</response>
    [Authorize]
    [HttpDelete("logout")]
    public async Task<ActionResult> Logout()
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return Unauthorized();
        }

        bool result = await _authManager.LogoutAsync(userId);
        return result ? Ok() : NotFound();
    }
}
