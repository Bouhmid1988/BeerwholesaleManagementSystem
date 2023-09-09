using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;

public interface IBeerService
{
    Task<Beer> GetBeerById(int id, CancellationToken cancellationToken);
    Task<Beer> CreateBeer(Beer newBeer, CancellationToken cancellationToken);
    Task DeleteBeer(Beer Beer, CancellationToken cancellationToken);
    Task<IEnumerable<Beer>> GetBeersByBrewery(int beerId, CancellationToken cancellationToken);
}
