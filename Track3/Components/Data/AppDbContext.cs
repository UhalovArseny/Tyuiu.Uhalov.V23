using Microsoft.EntityFrameworkCore;
using Track3.Components.Models;
using VirtualMuseum.Models;

namespace Track3.Components.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<VisitedExhibit> VisitedExhibits => Set<VisitedExhibit>();
    public DbSet<VisitedTour> VisitedTours => Set<VisitedTour>();

    public DbSet<Tour> Tours => Set<Tour>(); // ✅ ВАЖНО

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
