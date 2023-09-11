using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.DTO;

public class BeerDto
{
    public string Name { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal AlcoholContent { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public int BreweryId { get; set; }
}
