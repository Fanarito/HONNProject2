using System;

namespace VideotapeGalore.WebApi.Seed.Entities
{
    public class BorrowInfoSeedDto
    {
        // Tape id
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}