using System.ComponentModel.DataAnnotations;

namespace LearnWebAPI.Models;

public class FilterParams : Pagination
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? IsInStock { get; set; }
}