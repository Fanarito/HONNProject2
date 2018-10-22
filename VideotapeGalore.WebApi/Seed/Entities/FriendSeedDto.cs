using System.Collections.Generic;

namespace VideotapeGalore.WebApi.Seed.Entities
{
    public class FriendSeedDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public IEnumerable<BorrowInfoSeedDto> Tapes { get; set; }
    }
}