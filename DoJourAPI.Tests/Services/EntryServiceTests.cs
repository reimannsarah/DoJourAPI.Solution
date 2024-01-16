using Moq;
using DoJourAPI.Models;
using DoJourAPI.Repositories;
using DoJourAPI.Services;
using Xunit;

namespace DoJourAPI.Tests.Services;
public class EntryServiceTests
{
  private Mock<IEntryRepository> _entryRepositoryMock;
  private EntryService _entryService;

  public EntryServiceTests()
  {
    _entryRepositoryMock = new Mock<IEntryRepository>();
    _entryService = new EntryService(_entryRepositoryMock.Object);
  }

  [Fact]
  public async Task GetByAllEntriesAsync_ShouldReturnAllEntries()
  {
    var expectedEntries = new[]
    {
      new Entry { EntryId = 1, Title = "Entry 1" },
      new Entry { EntryId = 2, Title = "Entry 2" },
      new Entry { EntryId = 3, Title = "Entry 3" }
    };
    _entryRepositoryMock
      .Setup(x => x.GetAllAsync())
      .ReturnsAsync(expectedEntries);

    var actualEntries = await _entryService.GetAllEntriesAsync();

    Assert.Equal(expectedEntries, actualEntries);
  }

  [Fact]
  public async Task GetEntryByIdAsync_ShouldReturnEntryWithMatchingId()
  {
    var expectedEntry = new Entry { EntryId = 1, Title = "Entry 1" };
    _entryRepositoryMock
      .Setup(x => x.GetByIdAsync(1))
      .ReturnsAsync(expectedEntry);

    var actualEntry = await _entryService.GetEntryByIdAsync(1);

    Assert.Equal(expectedEntry, actualEntry);
  }

  [Fact]
  public async Task CreateEntryAsync_ShouldCreateNewEntry()
  {
    var entryToCreate = new Entry { EntryId = 1, Title = "Entry 1" };

    await _entryService.CreateEntryAsync(entryToCreate);

    _entryRepositoryMock.Verify(x => x.CreateAsync(entryToCreate));
  }

  [Fact]
  public async Task UpdateEntryAsync_ShouldUpdateEntry()
  {
    var entryToUpdate = new Entry { EntryId = 1, Title = "Entry 1" };

    await _entryService.UpdateEntryAsync(entryToUpdate);

    _entryRepositoryMock.Verify(x => x.UpdateAsync(entryToUpdate));
  }

  [Fact]
  public async Task DeleteEntryAsync_ShouldDeleteEntry()
  {
    var entryToDelete = new Entry { EntryId = 1, Title = "Entry 1" };

    await _entryService.DeleteEntryAsync(entryToDelete.EntryId);

    _entryRepositoryMock.Verify(x => x.DeleteAsync(entryToDelete.EntryId));
  }
}