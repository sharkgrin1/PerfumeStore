using LearnWebAPI.Models;

namespace LearnWebAPI.services;

public interface IPerfumeService
{
    List<Perfume> GetAll();

    Perfume? GetOne(int id);

    List<Perfume> GetByName(string name);

    void Create(Perfume perfume);

    void Update(int id, Perfume perfume);

    Perfume Delete(int id);
}