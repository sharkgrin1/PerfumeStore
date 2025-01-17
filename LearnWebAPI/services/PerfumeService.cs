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

    public List<Perfume> Filter(FilterParams filterParams)
    {
        IQueryable<Perfume> query = db.Perfumes;

        var name = filterParams.Name;
        if (name != null)
        {
            query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        }

        if (filterParams.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= filterParams.MinPrice.Value);
        }

        if (filterParams.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= filterParams.MaxPrice.Value);
        }

        if (filterParams.IsInStock.HasValue)
        {
            var inStock = filterParams.IsInStock.Value;
            query = inStock ? query.Where(x => x.Quantity > 0) : query.Where(x => x.Quantity == 0);
        }

        var sortBy = filterParams.SortBy;
        if (sortBy != null)
        {
            query = filterParams.SortOrder == SortOrder.ASC
                ? query.OrderBy(x => EF.Property<object>(x, sortBy))
                : query.OrderByDescending(x => EF.Property<object>(x, sortBy));
        }

        var size = filterParams.Size;
        return query.Skip(size * (filterParams.Page - 1)).Take(size).ToList();
    }

    public Perfume? GetOne(int id)
    {
        return db.Perfumes.Find(id);
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