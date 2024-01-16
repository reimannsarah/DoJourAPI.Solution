using Moq;
using DoJourAPI.Models;
using DoJourAPI.Repositories;
using DoJourAPI.Services;
using Xunit;
using DoJourAPI.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DoJourAPI.Tests.Controllers;

public class EntryControllerTests
{
  private Mock<IEntryService> _entryServiceMock;
  private EntriesController _entriesController;

  public EntryControllerTests()
  {
    _entryServiceMock = new Mock<IEntryService>();
    _entriesController = new EntriesController(_entryServiceMock.Object);
  }

  [Fact]
  public async Task GetAllEntries_ShouldReturnAllEntries()
  {
    var expectedEntries = new List<Entry>
    {
      new Entry { EntryId = 1, Title = "Entry 1" },
      new Entry { EntryId = 2, Title = "Entry 2" },
      new Entry { EntryId = 3, Title = "Entry 3" }
    };
    _entryServiceMock.Setup(service => service.GetAllEntriesAsync()).ReturnsAsync(expectedEntries);

    var result = await _entriesController.GetAllEntries();

    var okResult = Assert.IsType<OkObjectResult>(result);
    var actualEntries = Assert.IsType<List<Entry>>(okResult.Value);
    Assert.Equal(expectedEntries, actualEntries);
  }

  [Fact]
  public async Task GetEntryById_ShouldReturnEntryWithMatchingId()
  {
    var expectedEntry = new Entry { EntryId = 1, Title = "Entry 1" };
    _entryServiceMock.Setup(service => service.GetEntryByIdAsync(1)).ReturnsAsync(expectedEntry);

    var result = await _entriesController.GetEntryById(1);

    var okResult = Assert.IsType<OkObjectResult>(result);
    var actualEntry = Assert.IsType<Entry>(okResult.Value);
    Assert.Equal(expectedEntry, actualEntry);
  }

}