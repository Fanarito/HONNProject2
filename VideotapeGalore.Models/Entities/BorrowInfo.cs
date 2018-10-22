using System;

namespace VideotapeGalore.Models.Entities
{
    public class BorrowInfo
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Foreign Keys
        public int FriendId { get; set; }
        public int TapeId { get; set; }

        public virtual Friend Friend { get; set; }
        public virtual Tape Tape { get; set; }
    }
}