using Microsoft.AspNetCore.Mvc;
using DoJourAPI.Services;
using DoJourAPI.Models;

namespace DoJourAPI.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController : ControllerBase
{
  private readonly IUserService _userService;
  public UsersController(IUserService userService)
  {
    _userService = userService;
  }

  private readonly TokenService _tokenService;

  public UsersController(TokenService tokenService)
  {
    _tokenService = tokenService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterUser(User user)
  {
    var existingUser = await _userService.GetUserByEmailAsync(user.Email);
    if (existingUser != null)
    {
      return BadRequest("A user with this email already exists.");
    }

    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
    await _userService.CreateUserAsync(user);
    return Ok();
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginUser(User user)
  {
    var foundUser = await _userService.GetUserByEmailAsync(user.Email);
    if (foundUser == null)
    {
      return NotFound();
    }
    if (!BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password))
    {
      return Unauthorized();
    }

    var token = _tokenService.GenerateToken(foundUser);
    return Ok(new { Token = token });
  }
}