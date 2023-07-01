using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1;

public class ApplicationContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Rate> Rates { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
        Database.EnsureCreated();
    }
}