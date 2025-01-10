using LearnWebAPI.Models;
using LearnWebAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PerfumeController(IPerfumeService perfumeService) : ControllerBase
{
    [HttpGet]
    public List<Perfume> GetAllPerfume()
    {
        return perfumeService.GetAll();
    }

    [HttpGet]
    [Route("{id:int}")]
    public Perfume? GetPerfume(int id)
    {
        return perfumeService.GetOne(id);
    }
}