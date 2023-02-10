using Microsoft.EntityFrameworkCore;

namespace Archery.Model;

public class ArcheryContext : DbContext
{
    public DbSet<Parcour> Parcour { set; get; } = null!;

    public ArcheryContext(DbContextOptions<ArcheryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }
}