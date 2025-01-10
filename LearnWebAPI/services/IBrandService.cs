using LearnWebAPI.Models;

namespace LearnWebAPI.services;

public interface IBrandService
{
    List<Brand> GetAll();
}