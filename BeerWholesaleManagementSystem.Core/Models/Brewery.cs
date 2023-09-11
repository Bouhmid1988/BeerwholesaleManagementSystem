namespace BeerWholesaleManagementSystem.Core.Models;

public class Brewery
{
    public int BreweryId { get; set; }
    public required string Name { get; set; }
    public virtual List<Beer> Beers { get; set; }
}
