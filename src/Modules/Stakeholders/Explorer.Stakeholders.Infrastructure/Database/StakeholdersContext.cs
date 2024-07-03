using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Tourist> Tourists { get; set; }
    public DbSet<Author> Authors { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<Tourist>()
            .Property(item => item.Interests).HasColumnType("jsonb");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        ConfigureStakeholder(modelBuilder);

        modelBuilder.Ignore<Tour>();
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<Tourist>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Tourist>(t => t.UserId);

        modelBuilder.Entity<Author>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Author>(t => t.UserId);
    }
}