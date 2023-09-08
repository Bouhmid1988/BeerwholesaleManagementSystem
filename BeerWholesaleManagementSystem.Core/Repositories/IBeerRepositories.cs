using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Repositories;

public interface IBeerRepositories : IRepositories<Beer>
{
    Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken);

}
