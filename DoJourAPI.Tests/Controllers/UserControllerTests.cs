using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoJourAPI.Controllers;
using DoJourAPI.Services;
using DoJourAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

public class UsersControllerTests
{
  private readonly Mock<IUserService> _mockUserService;
  private readonly TokenService _tokenService;
  private readonly UsersController _controller;

  public UsersControllerTests()
  {
    _mockUserService = new Mock<IUserService>();
    var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
    {
        new KeyValuePair<string, string?>("JWT_SECRET_KEY", Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? string.Empty)
    })
    .Build();
    _tokenService = new TokenService(configuration);
    _controller = new UsersController(_tokenService, _mockUserService.Object);
  }

  [Fact]
  public async Task RegisterUser_ReturnsBadRequest_WhenModelStateIsInvalid()
  {
    var user = new User { Email = "invalid email", Password = "short" };
    _controller.ModelState.AddModelError("Email", "Invalid email format");
    _controller.ModelState.AddModelError("Password", "Password is too short");

    var result = await _controller.RegisterUser(user);

    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    var modelState = badRequestResult.Value as ModelStateDictionary;

    if (modelState != null) ;
    {
      if (modelState.ContainsKey("Email"))
      {
        var emailErrors = modelState["Email"].Errors;
        if (emailErrors != null && emailErrors.Count > 0 && emailErrors[0] != null)
        {
          Assert.Contains("Invalid email format", emailErrors[0].ErrorMessage);
        }
      }
    }
  }

  [Fact]
  public async Task RegisterUser_ReturnsOk_WhenUserDoesNotExist()
  {
    var user = new User { Email = "test@example.com", Password = "password" };
    _mockUserService.Setup(s => s.GetUserByEmailAsync(user.Email)).ReturnsAsync((User?)null);

    var result = await _controller.RegisterUser(user);

    Assert.IsType<OkResult>(result);
  }
}