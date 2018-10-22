using System.ComponentModel.DataAnnotations;

namespace VideotapeGalore.Models.InputModels
{
    public class FriendInputModel
    {
        [Required]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [MinLength(1)]
        public string Address { get; set; }    }
}