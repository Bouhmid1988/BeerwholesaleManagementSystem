using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.DTO;

public class QuoteBeerItemResponsetDto
{
    public int BeerId { get; set; }
    public required string Name { get; set; }
    public int BreweryId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
}
