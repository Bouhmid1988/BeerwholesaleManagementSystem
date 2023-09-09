namespace BeerWholesaleManagementSystem.Core.DTO;

public class SaleBeerDto
{
    public int BeerId { get; set; }
    public int WholesalerId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateSale { get; set; }
}
