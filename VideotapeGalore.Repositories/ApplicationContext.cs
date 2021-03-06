using Microsoft.EntityFrameworkCore;
using VideotapeGalore.Models.Entities;

namespace VideotapeGalore.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Tape> Tapes { get; set; }
        public DbSet<BorrowInfo> BorrowInfos { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>().HasMany(f => f.BorrowInfos).WithOne(b => b.Friend);
            modelBuilder.Entity<Tape>().HasMany(t => t.BorrowInfos).WithOne(b => b.Tape);

            modelBuilder.Entity<Review>().HasKey(r => new {r.FriendId, r.TapeId});
            modelBuilder.Entity<Review>().HasOne(r => r.Tape).WithMany(t => t.Reviews);
            modelBuilder.Entity<Review>().HasOne(r => r.Friend).WithMany(u => u.Reviews);

            modelBuilder.Entity<BorrowInfo>().Property(b => b.BorrowDate).HasDefaultValueSql("datetime('now')");
        }
    }
}