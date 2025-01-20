using LearnWebAPI.Models;

namespace LearnWebAPI.services;

public interface IBrandService
{
    List<Brand> GetAll();
    
    List<Brand> GetAll(MinPagination pagination);
    
    Brand? GetOne(int id);
}