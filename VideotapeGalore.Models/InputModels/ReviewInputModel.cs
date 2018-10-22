using System.ComponentModel.DataAnnotations;

namespace VideotapeGalore.Models.InputModels
{
    public class ReviewInputModel
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}