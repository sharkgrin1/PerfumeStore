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

    public List<Brand> GetAll(MinPagination pagination)
    {
        var size = pagination.Size!.Value;
        return db.Brands.Skip(size * (pagination.Page!.Value - 1)).Take(size).ToList();
    }

    public Brand? GetOne(int id)
    {
        return db.Brands.Find(id);
    }
}