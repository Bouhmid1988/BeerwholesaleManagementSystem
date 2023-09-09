using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services;

public class BeerService : IBeerService
{
    private readonly IBeerRepositories _beerRepositories;

    public BeerService(IBeerRepositories beerRepositories)
    {
        _beerRepositories = beerRepositories ?? throw new ArgumentNullException(nameof(_beerRepositories)); 
    }
    public async Task<Beer> CreateBeer(Beer newBeer, CancellationToken cancellationToken)
    {
        var existingBeer = await _beerRepositories.GetByIdAsync(newBeer.BeerId, cancellationToken);
        if (existingBeer != null)
            throw new InvalidOperationException("Beer with this ID already exists.");

        return await _beerRepositories.AddAsync(newBeer, cancellationToken);
    }

    public async Task DeleteBeer(Beer beer, CancellationToken cancellationToken)
    {
        var existingBeer = await _beerRepositories.GetByIdAsync(beer.BeerId, cancellationToken);
        if (existingBeer == null)
            throw new InvalidOperationException("The specified beer was not found.");

        await _beerRepositories.RemoveAsync(beer, cancellationToken);
    }
    public async Task<Beer> GetBeerById(int id, CancellationToken cancellationToken)
    {
        return await _beerRepositories.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken)
    {
        var ListBeer= await _beerRepositories.GetBeersByBrewery(breweryId, cancellationToken);
        if (!ListBeer.Any())
            throw new InvalidOperationException("Beer with this breweryId was not found.");
        return ListBeer;
    }
}
