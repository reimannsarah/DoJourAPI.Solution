namespace DoJourAPI.Tests.Repositories;

public class UserRepositoryTests
{
  [Fact]
  public async Task GetAllAsync_returnsAllUsers()
  {
    var helpers = new UserRepositoryTestHelpers();
    var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
    var userRepository = helpers.UserRepositoryInit(dbContextMock);

    var result = await userRepository.GetAllAsync();
    var resultList = result.ToList();

    Assert.Equal(3, resultList.Count());
  }
}