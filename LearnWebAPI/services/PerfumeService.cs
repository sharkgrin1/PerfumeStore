using LearnWebAPI.Models;
using LearnWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearnWebAPI.services;

public class PerfumeService(PerfumeContext db) : IPerfumeService
{
    public List<Perfume> GetAll()
    {
        return db.Perfumes.Include(x => x.Brand).ToList();
    }

    public Perfume? GetOne(int id)
    {
        return db.Perfumes.Find(id);
    }

    public List<Perfume> GetByName(string name)
    {
        return db.Perfumes.Where(x => x.Name.Contains(name)).ToList();
    }
}