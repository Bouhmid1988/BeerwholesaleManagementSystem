using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.Models;


public class QuoteRequest
{
    public int QuoteRequestId { get; set; }
    public int WholesalerId { get; set; }
    public DateTime DateQuote { get; set; }
    public  string Statuts { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }
    public virtual  Wholesaler Wholesaler { get; set; }
    public virtual List<QuoteRequestBeerDetail> QuoteRequestBeerDetail { get; set; }
}

