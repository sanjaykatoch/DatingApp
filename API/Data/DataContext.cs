using API.Entites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Appuser> Users { get; set; }
        public DbSet<UserLike> Likes{get;set;}

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
            .HasKey(x=>new{x.SourceUserId,x.LikedUserId});

         builder.Entity<UserLike>()
         .HasOne(s=>s.SourceUser)
         .WithMany(s=>s.LikedUsers)
         .HasForeignKey(s=>s.SourceUserId)
        .OnDelete(DeleteBehavior.Cascade);

          builder.Entity<UserLike>()
         .HasOne(s=>s.LikedUser)
         .WithMany(s=>s.LikedByUsers)
         .HasForeignKey(s=>s.LikedUserId)
         .OnDelete(DeleteBehavior.Cascade);
        }

    }
}