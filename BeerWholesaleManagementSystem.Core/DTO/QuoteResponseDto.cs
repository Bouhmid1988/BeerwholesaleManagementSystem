using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.DTO;

public class QuoteResponseDto
{
    public int WholesalerId { get; set; }
    public string Statuts { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }
    public List<QuoteBeerItemResponsetDto> QuoteItems { get; set; } 
    public DateTime RequestDate { get; set; } 
}
