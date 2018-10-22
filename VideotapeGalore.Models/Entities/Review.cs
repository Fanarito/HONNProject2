namespace VideotapeGalore.Models.Entities
{
    public class Review
    {
        public int FriendId { get; set; }
        public int TapeId { get; set; }
        public int Rating { get; set; }
        
        public virtual Friend Friend { get; set; }
        public virtual Tape Tape { get; set; }
    }
}