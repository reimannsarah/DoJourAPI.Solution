using DoJourAPI.Models;
using DoJourAPI.Repositories;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoJourAPI.Tests.Repositories;

public class UserRepositoryTestHelpers
{
  public DoJourAPIContext GetDbContext(User[] initialEntities)
  {
    var options = new DbContextOptionsBuilder<DoJourAPIContext>()
      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
      .Options;

    var context = new DoJourAPIContext(options);

    // Add your initial entities here
    context.Users.AddRange(GetInitialEntities());
    context.SaveChanges();

    return context;
  }
  public UserRepository UserRepositoryInit(DoJourAPIContext dbContext)
  {
    return new UserRepository(dbContext);
  }

  public User[] GetInitialEntities()
  {
    return new User[]
    {
      new User
      {
        UserId = 1,
        FirstName = "Test FirstName 1",
        LastName = "Test LastName 1",
        Email = "Test Email 1",
        Password = "Test Password 1"
      },
      new User
      {
        UserId = 2,
        FirstName = "Test FirstName 2",
        LastName = "Test LastName 2",
        Email = "Test Email 2",
        Password = "Test Password 2"
      },
      new User
      {
        UserId = 3,
        FirstName = "Test FirstName 3",
        LastName = "Test LastName 3",
        Email = "Test Email 3",
        Password = "Test Password 3"
      }
    };
  }
}