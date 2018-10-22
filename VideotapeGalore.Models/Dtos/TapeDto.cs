using System;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Models.Dtos
{
    public class TapeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Tape.TapeType Type { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Eidr { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
    }
}