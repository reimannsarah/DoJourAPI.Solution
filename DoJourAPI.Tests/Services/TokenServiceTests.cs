using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DoJourAPI.Models;
using System;

public class TokenServiceTests
{
  private readonly TokenService _tokenService;
  private readonly Mock<IConfiguration> _mockConfig;

  public TokenServiceTests()
  {
    DotNetEnv.Env.Load();
    _mockConfig = new Mock<IConfiguration>();
    _mockConfig.Setup(c => c["JwtConfig:Secret"]).Returns(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
    _mockConfig.Setup(c => c["JwtConfig:ExpireDays"]).Returns("7");
    _mockConfig.Setup(c => c["JwtConfig:Issuer"]).Returns("https://do-jour-api.azurewebsites.net/");
    _mockConfig.Setup(c => c["JwtConfig:Audience"]).Returns("https://do-jour-api.azurewebsites.net/");

    _tokenService = new TokenService(_mockConfig.Object);
  }

  [Fact]
  public void GenerateToken_ShouldReturnToken_WhenCalledWithValidUser()
  {
    Console.WriteLine(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")); // Add this line
    var user = new User { Email = "test@example.com", UserId = Guid.NewGuid() };

    var token = _tokenService.GenerateToken(user);

    Assert.NotNull(token);
    Assert.IsType<string>(token);
  }
}