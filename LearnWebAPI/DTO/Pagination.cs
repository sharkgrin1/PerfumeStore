using System.ComponentModel.DataAnnotations;

namespace LearnWebAPI.Models;

public class Pagination
{
    private const int MaxSize = 10;
    
    [Range(1, MaxSize)]
    public int Size { get; set; } = 5;

    public int Page { get; set; } = 1;
}