using LearnWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnWebAPI.Repositories;

public class PerfumeContext(DbContextOptions<PerfumeContext> options) : DbContext(options)
{
    public DbSet<Perfume> Perfumes { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .HasMany<Perfume>(x => x.Perfumes)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId);

        modelBuilder.Seed();
    }
}