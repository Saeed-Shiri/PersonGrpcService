

using Microsoft.EntityFrameworkCore;
using PersonSrv.Domain.Entities;

namespace PersonSrv.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.NationalCode).IsRequired().HasMaxLength(10);
            entity.Property(p => p.BirthDate).IsRequired();
        });
    }
}
