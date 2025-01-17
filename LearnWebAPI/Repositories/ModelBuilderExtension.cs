using LearnWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnWebAPI.Repositories;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Perfume>().HasData(
            new Perfume
            {
                Id = 1, Name = "Si Passion", BrandId = 1, PerfumeType = PerfumeType.Parfum, Quantity = 22,
                Price = 120.90m
            },
            new Perfume
            {
                Id = 2, Name = "Lost Cherry", BrandId = 2, PerfumeType = PerfumeType.EauFraiche, Quantity = 20,
                Price = 420m
            },
            new Perfume
            {
                Id = 3, Name = "Electric Cherry", BrandId = 2, PerfumeType = PerfumeType.Parfum, Quantity = 5,
                Price = 450m
            },
            new Perfume
            {
                Id = 4, Name = "Miss Dior", BrandId = 3, PerfumeType = PerfumeType.EauDeToilette, Quantity = 100,
                Price = 100m
            },
            new Perfume
            {
                Id = 5, Name = "Chance", BrandId = 4, PerfumeType = PerfumeType.EauDeParfum, Quantity = 10, Price = 200m
            }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Georgio Armani" },
            new Brand { Id = 2, Name = "Tom Ford" },
            new Brand { Id = 3, Name = "Dior" },
            new Brand { Id = 4, Name = "Channel" }
        );
    }
}