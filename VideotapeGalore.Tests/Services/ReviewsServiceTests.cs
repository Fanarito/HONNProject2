using System;
using System.Linq;
using System.Threading.Tasks;
using VideotapeGalore.Models.Entities;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Implementations;
using VideotapeGalore.Tests.DataFakers;
using Xunit;

namespace VideotapeGalore.Tests.Services
{
    public class ReviewsServiceTests : IDisposable
    {
        private readonly ApplicationContext _context;

        public ReviewsServiceTests()
        {
            _context = BogusContext.SetupDb();
        }

        [Fact]
        public async Task GetAllForTape_ShouldReturnAllForTape()
        {
            for (var i = 0; i < 10; i++)
            {
                _context.Add(new Review
                {
                    FriendId = i + 1,
                    TapeId = 1,
                    Rating = 5
                });
            }
            _context.SaveChanges();

            var service = new ReviewsService(_context);
            var reviewsForTape = await service.GetAllForTape(1);
            Assert.Equal(10, reviewsForTape.Count());
        }
        
        [Fact]
        public async Task GetAllForFriend_ShouldReturnAllForFriend()
        {
            for (var i = 0; i < 10; i++)
            {
                _context.Add(new Review
                {
                    FriendId = 1,
                    TapeId = i + 1,
                    Rating = 5
                });
            }
            _context.SaveChanges();

            var service = new ReviewsService(_context);
            var reviewsFromFriend = await service.GetAllForFriend(1);
            Assert.Equal(10, reviewsFromFriend.Count());
        }

        [Fact]
        public async Task GetFriendReviewOfTape_ShouldReturnCorrectReview()
        {
            _context.Add(new Review
            {
                FriendId = 1,
                TapeId = 2,
                Rating = 5
            });
            _context.SaveChanges();
            var service = new ReviewsService(_context);
            var review = await service.GetFriendReviewOfTape(1, 2);
            Assert.Equal(1, review.FriendId);
            Assert.Equal(2, review.TapeId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}