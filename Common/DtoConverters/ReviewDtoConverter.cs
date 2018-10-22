using System.Collections.Generic;
using System.Linq;
using VideotapeGalore.Models.Dtos;
using VideotapeGalore.Models.Entities;

namespace Common.DtoConverters
{
    public static class ReviewDtoConverter
    {
        public static ReviewDto ToDto(this Review item) =>
            new ReviewDto
            {
                FriendId = item.FriendId,
                TapeId = item.TapeId,
                Rating = item.Rating,
            };

        public static IEnumerable<ReviewDto> ToDtos(this IEnumerable<Review> items)
            => items.Select(i => i.ToDto());
    }
}