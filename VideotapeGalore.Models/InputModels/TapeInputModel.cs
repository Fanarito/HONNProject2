using System;
using System.ComponentModel.DataAnnotations;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Models.InputModels
{
    public class TapeInputModel
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        public Tape.TapeType Type { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [RegularExpression(@"10\.5240\/([A-Z]{4}-){5}[A-Z]")]
        public string Eidr { get; set; }
        [Required]
        [MinLength(1)]
        public string DirectorFirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string DirectorLastName { get; set; }    }
}