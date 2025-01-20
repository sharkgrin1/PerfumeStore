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

    public List<Perfume> Filter(FilterParams filter, Sorting sort, Pagination pagination)
    {
        IQueryable<Perfume> query = db.Perfumes;

        var name = filter.Name;
        if (name != null)
        {
            query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= filter.MaxPrice.Value);
        }

        if (filter.IsInStock.HasValue)
        {
            var inStock = filter.IsInStock.Value;
            query = inStock ? query.Where(x => x.Quantity > 0) : query.Where(x => x.Quantity == 0);
        }

        query = SortQuery(sort, query);

        return PaginationQuery(pagination, query).ToList();
    }

    public List<Perfume> Search(string? search, Sorting sort, Pagination pagination)
    {
        IQueryable<Perfume> query = db.Perfumes;
        if (search != null)
        {
            search = search.ToLower();
            query = query.Where(x=>x.Name.ToLower().Contains(search) ||
                                   x.Quantity.ToString().Equals(search) ||
                                   x.Id.ToString().Equals(search) ||
                                   x.Price.ToString().Equals(search) || 
                                   x.PerfumeType.ToString().ToLower().Contains(search));
        }

        query = SortQuery(sort, query);

        return PaginationQuery(pagination, query).ToList();
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

    private static IQueryable<Perfume> PaginationQuery(Pagination pagination, IQueryable<Perfume> query)
    {
        var size = pagination.Size;
        return query.Skip(size * (pagination.Page - 1)).Take(size);
    }

    private static IQueryable<Perfume> SortQuery(Sorting sort, IQueryable<Perfume> query)
    {
        var sortBy = sort.SortBy;
        if (sortBy != null)
        {
            query = sort.SortOrder == SortOrder.ASC
                ? query.OrderBy(x => EF.Property<object>(x, sortBy))
                : query.OrderByDescending(x => EF.Property<object>(x, sortBy));
        }

        return query;
    }
}