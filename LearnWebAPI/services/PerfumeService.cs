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

    public List<Perfume> GetAll(Pagination pagination)
    {
        return db.Perfumes.Skip(pagination.Size * (pagination.Page - 1)).Take(pagination.Size).ToList();
    }

    public Perfume? GetOne(int id)
    {
        return db.Perfumes.Find(id);
    }

    public List<Perfume> GetByName(string name)
    {
        return db.Perfumes.Where(x => x.Name.Contains(name)).ToList();
    }

    public void Create(Perfume perfume)
    {
        db.Perfumes.Add(perfume);
        db.SaveChanges();
    }

    public void Update(int id, Perfume perfume)
    {
        if (id != perfume.Id || !db.Perfumes.Any(x => x.Id == id)) throw new Exception("Perfume not found");
        db.Entry(perfume).State = EntityState.Modified;
        db.SaveChanges();
    }

    public Perfume Delete(int id)
    {
        var perfume = GetOne(id);
        if (perfume == null) throw new Exception("Perfume not found");
        db.Perfumes.Remove(perfume);
        db.SaveChanges();
        return perfume;
    }

    public void Delete(int[] ids)
    {
        var perfumes = db.Perfumes.Where(x => ids.Contains(x.Id));
        db.Perfumes.RemoveRange(perfumes);
        db.SaveChanges();
    }
}