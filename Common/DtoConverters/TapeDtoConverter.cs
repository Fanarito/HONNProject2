using System.Collections.Generic;
using System.Linq;
using VideotapeGalore.Models.Dtos;
using VideotapeGalore.Models.Entities;

namespace Common.DtoConverters
{
    public static class TapeDtoConverter
    {
        public static TapeDto ToDto(this Tape item) =>
            new TapeDto
            {
                Id = item.Id,
                Title = item.Title,
                Type = item.Type,
                ReleaseDate = item.ReleaseDate,
                Eidr = item.Eidr,
                DirectorFirstName = item.DirectorFirstName,
                DirectorLastName = item.DirectorLastName,
            };

        public static IEnumerable<TapeDto> ToDtos(this IEnumerable<Tape> items)
            => items.Select(i => i.ToDto());
    }
}
