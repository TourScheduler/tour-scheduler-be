using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Tour> Tours { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>()
        .HasOne<Author>()
        .WithMany(author => author.Tours)
        .HasForeignKey(tour => tour.AuthorId);

        modelBuilder.Ignore<Author>();

        modelBuilder.Entity<Tour>()
            .Property(item => item.KeyPoints).HasColumnType("jsonb");
    }
}