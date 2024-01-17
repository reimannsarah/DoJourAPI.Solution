using System.Linq;
using System.Threading.Tasks;
using DoJourAPI.Repositories;
using System.Collections.Generic;
using Xunit;
using DoJourAPI.Models;

namespace DoJourAPI.Tests.Repositories;
    public class EntryRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_returnsAllEntries()
        {
            var helpers = new EntryRepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var result = await entryRepository.GetAllAsync();
            var resultList = result.ToList();

            Assert.Equal(3, resultList.Count());
        }

        [Fact]
        public async Task GetByIdAsync_returnsEntryWithMatchingId()
        {
            var helpers = new EntryRepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);
            var testEntry = new Entry
            {
                EntryId = Guid.NewGuid(),
                Title = "Test Title 1",
                Subject = "Test Subject 1",
                Date = "Test Date 1",
                Text = "Test Text 1"
            };

            await entryRepository.CreateAsync(testEntry);
            
            var result = await entryRepository.GetByIdAsync(testEntry.EntryId);

            Assert.Equal(testEntry, result);
        }

        [Fact]
        public async Task CreateAsync_addsEntryToDatabase()
        {
            var helpers = new EntryRepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var newEntry = new Entry
            {
                EntryId = Guid.NewGuid(),
                Title = "Test Title 4",
                Subject = "Test Subject 4",
                Date = "Test Date 4",
                Text = "Test Text 4"
            };

            await entryRepository.CreateAsync(newEntry);

            var result = await entryRepository.GetByIdAsync(newEntry.EntryId);

            Assert.Equal(newEntry.EntryId, result.EntryId);
        }

        [Fact]
        public async Task UpdateAsync_updatesEntryInDatabase()
        {
            var helpers = new EntryRepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var testEntry = new Entry
            {
                EntryId = Guid.NewGuid(),
                Title = "Test Title 4",
                Subject = "Test Subject 4",
                Date = "Test Date 4",
                Text = "Test Text 4"
            };

            await entryRepository.CreateAsync(testEntry);

            var entryToUpdate = await entryRepository.GetByIdAsync(testEntry.EntryId);

            entryToUpdate.Title = "Updated Title 1";
            entryToUpdate.Subject = "Updated Subject 1";
            entryToUpdate.Date = "Updated Date 1";
            entryToUpdate.Text = "Updated Text 1";

            await entryRepository.UpdateAsync(entryToUpdate);

            var result = await entryRepository.GetByIdAsync(testEntry.EntryId);

            Assert.Equal("Updated Title 1", result.Title);
            Assert.Equal("Updated Subject 1", result.Subject);
            Assert.Equal("Updated Date 1", result.Date);
            Assert.Equal("Updated Text 1", result.Text);
        }

        [Fact]
        public async Task DeleteAsync_removesEntryFromDatabase()
        {
            var helpers = new EntryRepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var testEntry = new Entry
            {
                EntryId = Guid.NewGuid(),
                Title = "Test Title 4",
                Subject = "Test Subject 4",
                Date = "Test Date 4",
                Text = "Test Text 4"
            };

            await entryRepository.CreateAsync(testEntry);

            var beforeDelete = await entryRepository.GetAllAsync();
            var beforeDeleteList = beforeDelete.ToList();

            await entryRepository.DeleteAsync(testEntry.EntryId);

            var result = await entryRepository.GetAllAsync();
            var resultList = result.ToList();

            Assert.Equal(beforeDeleteList.Count() - 1, resultList.Count());
        }
    }
