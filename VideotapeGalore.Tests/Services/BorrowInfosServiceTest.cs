using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Filters;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Services.Interfaces;
using VideotapeGalore.Tests.DataFakers;
using Xunit;
using Xunit.Sdk;

namespace VideotapeGalore.Tests.Services
{
    public class BorrowInfosServiceTest : IDisposable
    {
        private readonly DbContextOptions<ApplicationContext> _options;

        public BorrowInfosServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "borrowInfoTest")
                .Options;

            using (var context = new ApplicationContext(_options))
            {
                for (var i = 0; i < 10; i++)
                {
                    context.Add(BogusDataGenerators.GetFriend());
                    context.Add(BogusDataGenerators.GetTape());
                }

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task FriendHasTapeOnLoan()
        {
            using (var context = new ApplicationContext(_options))
            {
                var borrowFilter = new BorrowFilter
                {
                    LoanDate = DateTime.Today.AddDays(-1)
                };

                context.Add(new BorrowInfo
                {
                    BorrowDate = DateTime.Now.AddDays(-1),
                    FriendId = 1,
                    TapeId = 1,
                });
                context.SaveChanges();

                var service = new BorrowInfosService(context);
                var friends = await service.GetFriendsMatchingBorrowFilter(borrowFilter);
                Assert.Single(friends);
            }
        }
    }
}