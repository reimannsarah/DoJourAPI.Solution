using Moq;
using DoJourAPI.Models;
using DoJourAPI.Repositories;
using DoJourAPI.Services;
using Xunit;

namespace DoJourAPI.Tests.Services;

public class UserServiceTests
{
  private readonly Mock<IUserRepository> _mockUserRepository;
  private readonly UserService _userService;

  public UserServiceTests()
  {
    _mockUserRepository = new Mock<IUserRepository>();
    _userService = new UserService(_mockUserRepository.Object);
  }

  [Fact]
  public async Task GetUserByIdAsync_ShouldReturnUserWithMatchingId()
  {
    var expectedUser = new User { UserId = Guid.NewGuid(), FirstName = "User 1" };
    _mockUserRepository
      .Setup(x => x.GetByIdAsync(expectedUser.UserId))
      .ReturnsAsync(expectedUser);

    var actualUser = await _userService.GetUserByIdAsync(expectedUser.UserId);

    Assert.Equal(expectedUser, actualUser);
  }

  [Fact]
  public async Task CreateUserAsync_ShouldCreateNewUser()
  {
    var userToCreate = new User { UserId = Guid.NewGuid(), FirstName = "User 1" };

    await _userService.CreateUserAsync(userToCreate);

    _mockUserRepository.Verify(x => x.CreateAsync(userToCreate));
  }
}