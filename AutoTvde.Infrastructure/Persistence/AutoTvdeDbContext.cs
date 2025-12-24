using AutoTvde.Domain.Common;
using AutoTvde.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace AutoTvde.Infrastructure.Persistence;

public class AutoTvdeDbContext : DbContext
{
    public AutoTvdeDbContext(DbContextOptions<AutoTvdeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Mediator> Mediators => Set<Mediator>();
    public DbSet<Quote> Quotes => Set<Quote>();
    public DbSet<Policy> Policies => Set<Policy>();
    public DbSet<CoverageItem> CoverageItems => Set<CoverageItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Vehicle>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Mediator>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Quote>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Policy>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<CoverageItem>()
            .HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Client>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Vehicle>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Mediator>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Quote>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Policy>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<CoverageItem>()
            .Property(x => x.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Client>()
            .HasIndex(x => x.Nif)
            .IsUnique();

        modelBuilder.Entity<Vehicle>()
            .HasIndex(x => x.LicensePlate)
            .IsUnique();
    }
}
