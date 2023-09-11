using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Repositories;

/// <summary>
/// Define the Beer Repository Interface
/// </summary>
public interface IBeerRepository : IRepository<Beer>
{
    /// <summary>
    /// Get Beers By Brewery asynchronously
    /// </summary>
    /// <param name="breweryId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken);
}
