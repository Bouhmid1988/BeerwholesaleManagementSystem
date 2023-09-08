using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.Models;

public class Beer
{
    public int BeerId { get; set; }
    public required string Name { get; set; }
    [Column(TypeName = "decimal(4, 2)")]
    public decimal AlcoholContent { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public int BreweryId { get; set; }
    public virtual Brewery? Brewery { get; set; }
    public virtual List<Stock>? Stocks { get; set; }
}
