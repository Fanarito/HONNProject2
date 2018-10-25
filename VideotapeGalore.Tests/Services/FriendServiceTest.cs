using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Tests.DataFakers;
using Xunit;

namespace VideotapeGalore.Tests.Services
{
    public class FriendServiceTest : IDisposable
    {
        private readonly ApplicationContext _context;

        public FriendServiceTest()
        {
            _context = BogusContext.SetupDb();
        }

        [Fact]
        public async Task TapesThatAreOnLoan()
        {
            for (var i = 0; i < 5; i++)
            {
                _context.Add(new BorrowInfo
                {
                    FriendId = 1,
                    TapeId = i + 1,
                    BorrowDate = DateTime.Now.AddDays(-2)
                });
            }
            _context.SaveChanges();
            
            var service = new FriendsService(_context);
            var tapesLoan = await service.TapesOnLoan(1);
            Assert.Equal(5, tapesLoan.Count());
        }

        [Fact]
        public async Task Register_Tape()
        {  
            var service = new FriendsService(_context);
            await service.RegisterTape(4,1);
            Assert.Single(await service.TapesOnLoan(4));
        }

        [Fact]
        public async Task Return_Tape()
        {   
            var service = new FriendsService(_context);
            await service.RegisterTape(4, 3);
            await service.ReturnTape(4, 3);
            Assert.Empty(await service.TapesOnLoan(4));

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}