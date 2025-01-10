using LearnWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnWebAPI.Repositories;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Perfume>().HasData(
            new Perfume { Id = 1, Name = "Si Passion", BrandId = 1, PerfumeType = PerfumeType.Parfum, Quantity = 22 },
            new Perfume { Id = 2, Name = "Lost Cherry", BrandId = 2, PerfumeType = PerfumeType.EauFraiche, Quantity = 20 },
            new Perfume
            {
                Id = 3, Name = "Electric Cherry", BrandId = 2, PerfumeType = PerfumeType.Parfum, Quantity = 5
            },
            new Perfume { Id = 4, Name = "Miss Dior", BrandId = 3, PerfumeType = PerfumeType.EauDeToilette, Quantity = 100 },
            new Perfume { Id = 5, Name = "Chance", BrandId = 4, PerfumeType = PerfumeType.EauDeParfum, Quantity = 10 }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Georgio Armani" },
            new Brand { Id = 2, Name = "Tom Ford" },
            new Brand { Id = 3, Name = "Dior" },
            new Brand { Id = 4, Name = "Channel" }
        );
    }
}