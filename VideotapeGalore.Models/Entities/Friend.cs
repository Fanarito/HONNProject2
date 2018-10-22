using System.Collections.Generic;

namespace VideotapeGalore.Models.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual IEnumerable<BorrowInfo> BorrowInfos { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
    }
}