using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services;

public class BeerServices : IBeerService
{
    private readonly IBeerRepositories _beerRepository;

    public BeerServices(IBeerRepositories beerRepository)
    {
        _beerRepository = beerRepository ?? throw new ArgumentNullException(nameof(_beerRepository)); 
    }
    public async Task<Beer> CreateBeer(Beer newBeer, CancellationToken cancellationToken)
    {
        var existingBeer = await _beerRepository.GetByIdAsync(newBeer.BeerId, cancellationToken);
        if (existingBeer != null)
            throw new InvalidOperationException("Beer with this ID already exists.");

        return await _beerRepository.AddAsync(newBeer, cancellationToken);
    }

    public async Task DeleteBeer(Beer beer, CancellationToken cancellationToken)
    {
        var existingBeer = await _beerRepository.GetByIdAsync(beer.BeerId, cancellationToken);
        if (existingBeer == null)
            throw new InvalidOperationException("The specified beer was not found.");

        await _beerRepository.RemoveAsync(beer, cancellationToken);
    }
    public async Task<Beer> GetBeerById(int id, CancellationToken cancellationToken)
    {
        return await _beerRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken)
    {
        var ListBeer= await _beerRepository.GetBeersByBrewery(breweryId, cancellationToken);
        if (!ListBeer.Any())
            throw new InvalidOperationException("Beer with this breweryId was not found.");
        return ListBeer;
    }
}
