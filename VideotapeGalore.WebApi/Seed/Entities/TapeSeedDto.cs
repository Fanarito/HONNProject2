using System;

namespace VideotapeGalore.WebApi.Seed.Entities
{
    public class TapeSeedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Eidr { get; set; }
    }
}