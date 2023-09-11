using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;
/// <summary>
/// Define the Beer Service Interface
/// </summary>
public interface IBeerService
{
    /// <summary>
    /// Gets a beer by its identifier.
    /// </summary>
    /// <param name="id">The beer's identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The corresponding beer.</returns>
    Task<Beer> GetBeerById(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new beer.
    /// </summary>
    /// <param name="newBeer">The new beer to create.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The created beer.</returns>
    Task<Beer> CreateBeer(Beer newBeer, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a beer.
    /// </summary>
    /// <param name="beer">The beer to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    Task DeleteBeer(Beer Beer, CancellationToken cancellationToken);

    /// <summary>
    /// Gets beers by brewery.
    /// </summary>
    /// <param name="breweryId">The brewery's identifier.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The list of beers from the brewery.</returns>
    Task<IEnumerable<Beer>> GetBeersByBrewery(int beerId, CancellationToken cancellationToken);
}
