using Core;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService UserService { get; }
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        this.UserService = userService;
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<List<UserReadDto>> Get([FromQuery] Pagination? pagination)
    {
        _logger.LogInformation("Providing request for all users");

        var result = UserService.GetAll(pagination);

        if (result == null || !result.Any())
        {
            return NoContent();
        }

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<UserReadDto> GetBy(int id)
    {
        _logger.LogInformation("Providing request for user with id {Id}", id);

        var result = UserService.GetById(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Add(UserCreateDto user)
    {
        _logger.LogInformation("Providing request for adding user with {UserEmail}", user.Email);

        UserService.Add(user);

        return Ok("User added successfully");
    }
    
    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Update(int id, UserUpdateDto userToUpdate)
    {
        _logger.LogInformation("Providing request for updating user with id {Id}", id);

        UserService.Update(id, userToUpdate);

        return Ok("User updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Delete(int id)
    {
        _logger.LogInformation("Providing request for deleting user with id {Id}", id);

        UserService.Delete(id);

        return Ok("User deleted successfully");
    }
}