using DoJourAPI.Models;
using DoJourAPI.Repositories;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoJourAPI.Tests.Repositories
{
    public class EntryRepositoryTestHelpers
    {
        public DoJourAPIContext GetDbContext(Entry[] initialEntities)
        {
        var options = new DbContextOptionsBuilder<DoJourAPIContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new DoJourAPIContext(options);

        // Add your initial entities here
        context.Entries.AddRange(GetInitialEntities());
        context.SaveChanges();

        return context;
        }
        public EntryRepository EntryRepositoryInit(DoJourAPIContext dbContext)
        {
            return new EntryRepository(dbContext);
        }

        public Entry[] GetInitialEntities()
        {
            return new Entry[]
            {
                new Entry
                {
                    EntryId = 1,
                    Title = "Test Title 1",
                    Subject = "Test Subject 1",
                    Date = "Test Date 1",
                    Text = "Test Text 1"
                },
                new Entry
                {
                    EntryId = 2,
                    Title = "Test Title 2",
                    Subject = "Test Subject 2",
                    Date = "Test Date 2",
                    Text = "Test Text 2"
                },
                new Entry
                {
                    EntryId = 3,
                    Title = "Test Title 3",
                    Subject = "Test Subject 3",
                    Date = "Test Date 3",
                    Text = "Test Text 3"
                }
            };
        }
    }
}
