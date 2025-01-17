using LearnWebAPI.Models;

namespace LearnWebAPI.services;

public interface IPerfumeService
{
    List<Perfume> GetAll();
    
    List<Perfume> Filter(FilterParams filterParams);

    Perfume? GetOne(int id);

    void Create(Perfume perfume);

    void Update(int id, Perfume perfume);

    Perfume Delete(int id);

    void Delete(int[] ids);
}