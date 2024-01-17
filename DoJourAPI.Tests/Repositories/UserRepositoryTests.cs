using DoJourAPI.Models;
using DoJourAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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

  [Fact]
  public async Task GetByIdAsync_returnsUserWithMatchingId()
  {
    var helpers = new UserRepositoryTestHelpers();
    var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
    var userRepository = helpers.UserRepositoryInit(dbContextMock);
    var testUser = new User
    {
      UserId = Guid.NewGuid(),
      FirstName = "Test First Name 1",
      LastName = "Test Last Name 1",
      Email = "Test Email 1",
      Password = "Test Password 1"
    };

    await userRepository.CreateAsync(testUser);

    var result = await userRepository.GetByIdAsync(testUser.UserId);

    Assert.Equal(testUser, result);
  }

  [Fact]
  public async Task CreateAsync_createsNewUser()
  {
    var helpers = new UserRepositoryTestHelpers();
    var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
    var userRepository = helpers.UserRepositoryInit(dbContextMock);
    var testUser = new User
    {
      UserId = Guid.NewGuid(),
      FirstName = "Test First Name 1",
      LastName = "Test Last Name 1",
      Email = "Test Email 1",
      Password = "Test Password 1"
    };

    await userRepository.CreateAsync(testUser);

    var result = await userRepository.GetByIdAsync(testUser.UserId);

    Assert.Equal(testUser, result);
  }

  [Fact]
  public async Task UpdateAsync_updatesUser()
  {
    var helpers = new UserRepositoryTestHelpers();
    var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
    var userRepository = helpers.UserRepositoryInit(dbContextMock);
    var testUser = new User
    {
      UserId = Guid.NewGuid(),
      FirstName = "Test First Name 1",
      LastName = "Test Last Name 1",
      Email = "Test Email 1",
      Password = "Test Password 1"
    };

    await userRepository.CreateAsync(testUser);

    testUser.FirstName = "Test First Name 2";
    testUser.LastName = "Test Last Name 2";
    testUser.Email = "Test Email 2";
    testUser.Password = "Test Password 2";

    await userRepository.UpdateAsync(testUser);

    var result = await userRepository.GetByIdAsync(testUser.UserId);

    Assert.Equal(testUser, result);
  }

  [Fact]
  public async Task DeleteAsync_deletesUser()
  {
    var helpers = new UserRepositoryTestHelpers();
    var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
    var userRepository = helpers.UserRepositoryInit(dbContextMock);
    var testUser = new User
    {
      UserId = Guid.NewGuid(),
      FirstName = "Test First Name 1",
      LastName = "Test Last Name 1",
      Email = "Test Email 1",
      Password = "Test Password 1"
    };

    await userRepository.CreateAsync(testUser);

    await userRepository.DeleteAsync(testUser.UserId);

    var result = await userRepository.GetByIdAsync(testUser.UserId);

    Assert.Null(result);
  }
}