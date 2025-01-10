using System.Text.Json.Serialization;

namespace LearnWebAPI.Models;

public class Perfume
{
    public required int Id { get; init; }
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required PerfumeType PerfumeType { get; set; }
    public required int BrandId { get; set; }
    
    [JsonIgnore]
    public virtual Brand? Brand { get; set; }
}