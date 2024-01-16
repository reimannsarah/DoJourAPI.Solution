using System.Linq;
using System.Threading.Tasks;
using DoJourAPI.Repositories;
using System.Collections.Generic;
using Xunit;

namespace DoJourAPI.Tests.Repositories
{
    public class EntryRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_returnsAllEntries()
        {
            var helpers = new RepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var result = await entryRepository.GetAllAsync();
            var resultList = result.ToList();

            Assert.Equal(3, resultList.Count());
        }

        [Fact]
        public async Task GetByIdAsync_returnsEntryWithMatchingId()
        {
            var helpers = new RepositoryTestHelpers();
            var dbContextMock = helpers.GetDbContext(helpers.GetInitialEntities());
            var entryRepository = helpers.EntryRepositoryInit(dbContextMock);

            var result = await entryRepository.GetByIdAsync(1);

            Assert.Equal(1, result.EntryId);
        }
    }
}
