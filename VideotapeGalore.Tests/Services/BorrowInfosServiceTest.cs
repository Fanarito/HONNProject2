using System;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Filters;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Tests.DataFakers;
using Xunit;

namespace VideotapeGalore.Tests.Services
{
    public class BorrowInfosServiceTest : IDisposable
    {
        private readonly ApplicationContext _context;

        public BorrowInfosServiceTest()
        {
            _context = BogusContext.SetupDb();
        }

        [Fact]
        public async Task FriendHasTapeOnLoan()
        {
            _context.Add(new BorrowInfo
            {
                BorrowDate = DateTime.Now.AddDays(-2),
                FriendId = 1,
                TapeId = 1,
            });
            _context.SaveChanges();

            var service = new BorrowInfosService(_context);
            var friends = await service.GetFriendsMatchingBorrowFilter(new BorrowFilter
            {
                LoanDate = DateTime.Today.AddDays(-1)
            });
            Assert.Single(friends);
        }

        [Fact]
        public async Task FriendHasOverdueTapeOnLoan()
        {
            _context.Add(new BorrowInfo
            {
                BorrowDate = DateTime.Now.AddDays(-100),
                FriendId = 1,
                TapeId = 1,
            });
            _context.SaveChanges();

            var service = new BorrowInfosService(_context);
            var friends = await service.GetFriendsMatchingBorrowFilter(new BorrowFilter
            {
                LoanDate = DateTime.Today.AddDays(-1),
                LoanDuration = 50
            });
            Assert.Single(friends);
        }

        [Fact]
        public async Task TapeIsOverdue()
        {
            _context.Add(new BorrowInfo
            {
                BorrowDate = DateTime.Now.AddDays(-100),
                FriendId = 1,
                TapeId = 1,
            });
            _context.SaveChanges();

            var service = new BorrowInfosService(_context);
            var tapes = await service.GetTapesMatchingBorrowFilter(new BorrowFilter
            {
                LoanDate = DateTime.Today.AddDays(-1),
                LoanDuration = 50
            });
            Assert.Single(tapes);
        }

        [Fact]
        public async Task TapeIsOnLoan()
        {
            _context.Add(new BorrowInfo
            {
                BorrowDate = DateTime.Now.AddDays(-1),
                FriendId = 1,
                TapeId = 1,
            });
            _context.SaveChanges();

            var service = new BorrowInfosService(_context);
            var tapes = await service.GetTapesMatchingBorrowFilter(new BorrowFilter
            {
                LoanDate = DateTime.Today.AddDays(-1)
            });
            Assert.Single(tapes);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}