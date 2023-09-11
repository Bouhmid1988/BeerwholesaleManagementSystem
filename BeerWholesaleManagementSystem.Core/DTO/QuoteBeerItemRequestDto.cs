using BeerWholesaleManagementSystem.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.DTO;

public class QuoteBeerItemRequestDto
{
    public int QuoteRequestId { get; set; }
    public int BeerId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public int Quantity { get; set; }
 
}
