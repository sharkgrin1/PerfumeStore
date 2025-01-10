using LearnWebAPI.Models;
using LearnWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnWebAPI.services;

public class BrandService(PerfumeContext db) : IBrandService
{
    public List<Brand> GetAll()
    {
        return db.Brands.Include(x => x.Perfumes).ToList();
    }

    public Brand? GetOne(int id)
    {
        return db.Brands.Find(id);
    }
}