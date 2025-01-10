using LearnWebAPI.Models;

namespace LearnWebAPI.services;

public interface IPerfumeService
{
    List<Perfume> GetAll();
    
    Perfume? GetOne(int id);
    
    List<Perfume> GetByName(string name);
}