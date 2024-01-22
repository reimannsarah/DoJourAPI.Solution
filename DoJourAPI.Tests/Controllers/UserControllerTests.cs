using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoJourAPI.Controllers;
using DoJourAPI.Services;
using DoJourAPI.Models;

public class UsersControllerTests
{
  private readonly Mock<IUserService> _mockUserService;
  private readonly UsersController _controller;

  public UsersControllerTests()
  {
    _mockUserService = new Mock<IUserService>();
    _controller = new UsersController(_mockUserService.Object);
  }

  [Fact]
  public async Task RegisterUser_ReturnsBadRequest_WhenUserExists()
  {
    var user = new User { Email = "test@example.com", Password = "password" };
    _mockUserService.Setup(s => s.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);

    var result = await _controller.RegisterUser(user);

    Assert.IsType<BadRequestObjectResult>(result);
  }

  [Fact]
  public async Task RegisterUser_ReturnsOk_WhenUserDoesNotExist()
  {
    var user = new User { Email = "test@example.com", Password = "password" };
    _mockUserService.Setup(s => s.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);

    var result = await _controller.RegisterUser(user);

    Assert.IsType<OkResult>(result);
  }
}