using Microsoft.AspNetCore.Mvc;
using DoJourAPI.Services;
using DoJourAPI.Models;

namespace DoJourAPI.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController : ControllerBase
{
  private readonly TokenService _tokenService;
  private readonly IUserService _userService;

  public UsersController(TokenService tokenService, IUserService userService)
  {
    _tokenService = tokenService;
    _userService = userService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterUser(User user)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    try
    {
      var existingUser = await _userService.GetUserByEmailAsync(user.Email);
      if (existingUser != null)
      {
        return BadRequest("A user with this email already exists.");
      }

      user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
      await _userService.CreateUserAsync(user);

      var token = _tokenService.GenerateToken(user);

      return Ok(new { token, user.UserId, user.FirstName, message = "User registered successfully" });
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> LoginUser(User user)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    try
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
    catch (Exception ex)
    {
      return StatusCode(500, $"Internal server error: {ex.Message}");
    }
  }
}