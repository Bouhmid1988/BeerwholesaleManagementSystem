using static BeerWholesaleManagementSystem.Core.Models.CommandRequest;

namespace BeerWholesaleManagementSystem.Core.Models;

public class Wholesaler
{
    public int WholesalerId { get; set; }
    public required string Name { get; set; }
    public virtual List<Stock>? Stocks { get; set; }
    public virtual List<CommandeRequest>? CommandRequest { get; set; }
}
