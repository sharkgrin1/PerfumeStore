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
    public ActionResult GetPerfume(int id)
    {
        var perfume = perfumeService.GetOne(id);
        return perfume is not null ? Ok(perfume) : NotFound();
    }

    [HttpGet("filter")]
    public List<Perfume> FilterPerfume([FromQuery] string name)
    {
        return perfumeService.GetByName(name);
    }
}