namespace BeerWholesaleManagementSystem.Core.Models;

public class SaleBeer
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public int WholesalerId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateSale { get; set; }

    public Wholesaler? Wholesaler { get; set; }
}
