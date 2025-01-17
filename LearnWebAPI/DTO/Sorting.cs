namespace LearnWebAPI.Models;

public class Sorting
{
    public string? SortBy { get; set; }
    public required SortOrder SortOrder { get; set; } = SortOrder.ASC;
}