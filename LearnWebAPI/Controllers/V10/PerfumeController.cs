using Asp.Versioning;
using LearnWebAPI.Models;
using LearnWebAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebAPI.Controllers.V10;

[ApiVersion("1.0")]
// [Route("v{version:apiVersion}/[controller]")]
[Route("[controller]")]
[ApiController]
public class PerfumeController(IPerfumeService perfumeService) : ControllerBase
{
    [HttpGet]
    public List<Perfume> FilterPerfume(
        [FromQuery] FilterParams filter,
        [FromQuery] Sorting sort,
        [FromQuery] Pagination pagination)
    {
        return perfumeService.Filter(filter, sort, pagination);
    }

    [HttpGet("search")]
    public List<Perfume> SearchPerfume(
        [FromQuery] string? search,
        [FromQuery] Sorting sort,
        [FromQuery] Pagination pagination)
    {
        return perfumeService.Search(search, sort, pagination);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult GetPerfume([FromRoute] int id)
    {
        var perfume = perfumeService.GetOne(id);
        return perfume is not null ? Ok(perfume) : NotFound();
    }

    [HttpPost]
    public ActionResult CreatePerfume([FromBody] Perfume perfume)
    {
        perfumeService.Create(perfume);
        return CreatedAtAction(nameof(GetPerfume), new { id = perfume.Id }, perfume);
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

    [HttpDelete("multiple")]
    public ActionResult DeleteMultiplePerfumes([FromQuery] int[] ids)
    {
        perfumeService.Delete(ids);
        return Ok();
    }
}