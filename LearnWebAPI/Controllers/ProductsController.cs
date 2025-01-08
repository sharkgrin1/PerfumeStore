using Microsoft.AspNetCore.Mvc;

namespace LearnWebAPI.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public string GetProducts()
    {
        return "Hello World!";
    }

    [HttpGet]
    [Route("/{id}")]
    public string GetProduct(int id)
    {
        return "Hello World!2";
    }
}