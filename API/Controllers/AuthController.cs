using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Services;
using Core.Models;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService; // Assume this service exists for user validation
    private readonly ITokenService _tokenService; // Assume this service exists for token generation

    public AuthController(IConfiguration configuration, IUserService userService, ITokenService tokenService)
    {
        _configuration = configuration;
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        var authenticatedUser = _userService.Authenticate(loginDto.Username, loginDto.Password); // Implement this inside IUserService

        if (authenticatedUser == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.CreateToken(authenticatedUser); // Implement this inside ITokenService
        return Ok(new { Token = token });
    }

}
