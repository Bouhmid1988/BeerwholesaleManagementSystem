namespace BeerWholesaleManagementSystem.Core.Models;

public class Wholesaler
{
    public int WholesalerId { get; set; }
    public string Name { get; set; }
    public virtual List<Stock> Stocks { get; set; }
    public virtual List<QuoteRequest> CommandRequest { get; set; }
}
