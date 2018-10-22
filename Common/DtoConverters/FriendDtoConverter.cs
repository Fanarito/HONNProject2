using System.Collections.Generic;
using System.Linq;
using VideotapeGalore.Models.Dtos;
using VideotapeGalore.Models.Entities;

namespace Common.DtoConverters
{
    public static class FriendDtoConverter
    {
        public static FriendDto ToDto(this Friend item) =>
            new FriendDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                Address = item.Address,
            };

        public static IEnumerable<FriendDto> ToDtos(this IEnumerable<Friend> items)
            => items.Select(i => i.ToDto());
    }
}