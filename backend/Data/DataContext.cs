using Microsoft.EntityFrameworkCore;

using backend.Models;

namespace backend.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<User> Users{get; set;}
        public DbSet<Bedroom> Bedrooms{get; set;}
        public DbSet<Reserve> Reserves{get; set;}


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Manager>()
        //         .HasOne(p => p.User)
        //         .WithMany(b => b.Managers)
        //         .HasForeignKey(p => p.UserForeignKey);
        // }
        
    }
}