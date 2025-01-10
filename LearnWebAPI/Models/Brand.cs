namespace LearnWebAPI.Models;

public class Brand
{
    public required int Id { get; init; }
    public required string Name { get; set; }
    
    public virtual List<Perfume> Perfumes { get; set; } = [];
}