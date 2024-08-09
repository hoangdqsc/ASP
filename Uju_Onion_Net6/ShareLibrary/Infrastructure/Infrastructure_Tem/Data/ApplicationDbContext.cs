using Core_Temp.Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Temp.Data
{

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Define your model relationships and configurations here
    }

   
}

}
