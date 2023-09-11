using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.Models;

public class QuoteRequestBeerDetail
{
    public int QuoteRequestId { get; set; }
    public int BeerId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public int Quantity { get; set; }
    public QuoteRequest QuoteRequest { get; set; }
    public Beer Beer { get; set; }
}
