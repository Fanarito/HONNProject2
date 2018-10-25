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
    public class RecommendationServiceTests : IDisposable
    {
        private readonly ApplicationContext _context;

        public RecommendationServiceTests()
        {
            _context = BogusContext.SetupDb();
        }

        [Fact]
        public async Task NoReviews_ShouldGet10Recommendations()
        {
            var service = new RecommendationService(_context);
            var recommendations = await service.GetRecommendations(1);
            Assert.Equal(10, recommendations.Count());
        }

        [Fact]
        public async Task OneReview_ReviewedTapeShouldBeFirst()
        {
            _context.Add(new Review
            {
                FriendId = 2,
                TapeId = 1,
                Rating = 5
            });
            _context.SaveChanges();

            var service = new RecommendationService(_context);
            var recommendations = await service.GetRecommendations(1);

            var enumerable = recommendations as Tape[] ?? recommendations.ToArray();
            Assert.Equal(10, enumerable.Count());
            Assert.Equal(1, enumerable.First().Id);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}