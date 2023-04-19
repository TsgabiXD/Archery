using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Archery.Model;

public class ArcheryContext : DbContext
{
    public DbSet<Parcour> Parcour { set; get; } = null!;
    public DbSet<User> User { set; get; } = null!;
    public DbSet<Target> Target { set; get; } = null!;
    public DbSet<Event> Event { set; get; } = null!;
    public DbSet<Mapping> Mapping { set; get; } = null!;


    public DbSet<IdentityUser> IdentityUser { set; get; } = null!;

    public ArcheryContext(DbContextOptions<ArcheryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
         .Entity<User>()
         .HasData(new
         {
             Id = 1,
             FirstName = "Tobias",
             LastName = "Schachner",
             NickName = "TsgabiXD",
             Role = "Admin",
             Hist = false
         },
         new
         {
             Id = 2,
             FirstName = "Luka",
             LastName = "Walkner",
             NickName = "woiges",
             Role = "Admin",
             Hist = false
         },
         new
         {
             Id = 3,
             FirstName = "Johannes",
             LastName = "R�lz",
             NickName = "JoRole",
             Role = "Admin",
             Hist = false
         });
    }
}