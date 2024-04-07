using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Base.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Weather> Weather { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении

        modelBuilder.Entity<Weather>()
            .HasKey(b => b.Id)
            .HasName("PK_WheatherId");

        base.OnModelCreating(modelBuilder);
    }
}
