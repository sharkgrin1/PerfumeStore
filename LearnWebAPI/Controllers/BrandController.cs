using LearnWebAPI.Models;
using LearnWebAPI.services;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController(IBrandService brandService) : ControllerBase
{
    [HttpGet]
    public ActionResult GetAllBrands()
    {
        return Ok(brandService.GetAll());
    }
}