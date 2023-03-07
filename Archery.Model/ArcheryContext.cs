using Microsoft.EntityFrameworkCore;

namespace Archery.Model;

public class ArcheryContext : DbContext
{
    public DbSet<Parcour> Parcour { set; get; } = null!;
    public DbSet<User> User { set; get; } = null!;

    public ArcheryContext(DbContextOptions<ArcheryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Parcour>()
            .HasData(new
            {
                Id = 1,
                Name = "Kirchschlag",
                AnimalNumber = 30
            });

        modelBuilder
            .Entity<User>()
            .HasData(new
            {
                Id = 1,
                FirstName = "Tobias",
                LastName = "Schachner",
                NickName = "TsgabiXD",
                Role = "Admin"
            },
            new
            {
                Id = 2,
                FirstName = "Luka",
                LastName = "Walkner",
                NickName = "woiges",
                Role = "Admin"
            },
            new
            {
                Id = 3,
                FirstName = "Johannes",
                LastName = "Rölz",
                NickName = "JoRole",
                Role = "Admin"
            });
    }
}