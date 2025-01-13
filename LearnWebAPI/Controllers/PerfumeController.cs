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
    public ActionResult GetPerfume([FromRoute] int id)
    {
        var perfume = perfumeService.GetOne(id);
        return perfume is not null ? Ok(perfume) : NotFound();
    }

    [HttpGet("filter")]
    public List<Perfume> FilterPerfume([FromQuery] string name)
    {
        return perfumeService.GetByName(name);
    }

    [HttpPost]
    public ActionResult CreatePerfume([FromBody] Perfume perfume)
    {
        perfumeService.Create(perfume);
        return CreatedAtAction(nameof(GetPerfume), new{id = perfume.Id}, perfume);
    }

    [HttpPut("{id:int}")]
    public ActionResult UpdatePerfume(int id, [FromBody] Perfume perfume)
    {
        perfumeService.Update(id, perfume);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeletePerfume(int id)
    {
        var perfume = perfumeService.Delete(id);
        return Ok(perfume);
    }
}