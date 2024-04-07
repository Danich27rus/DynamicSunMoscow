using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Base.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Weather> Weathers { get; set; }
    public DbSet<WindDirection> WindDirections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weather>()
            .HasKey(b => b.Id)
            .HasName("PK_WeatherId");

        modelBuilder.Entity<WindDirection>()
            .HasKey(b => b.Id)
            .HasName("PK_WindDirectionId");

        modelBuilder.Entity<Weather>()
            .HasMany(w => w.WindDirections)
            .WithOne();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
