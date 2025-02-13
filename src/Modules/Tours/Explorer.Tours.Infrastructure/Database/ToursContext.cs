﻿using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<TourProblem> TourProblems { get; set; }

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

        modelBuilder.Entity<Report>()
            .Property(item => item.BestSellingTours).HasColumnType("jsonb");

        modelBuilder.Entity<Report>()
            .Property(item => item.UnsoldedTours).HasColumnType("jsonb");

        modelBuilder.Entity<TourProblem>()
            .Property(item => item.Events).HasColumnType("jsonb");
    }
}