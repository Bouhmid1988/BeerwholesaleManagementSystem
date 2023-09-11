using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using BeerWholesaleManagementSystem.Core.Services;

namespace BeerWholesaleManagementSystem.Services.Services
{
    /// <summary>
    /// Represents a service for managing beer-related operations.
    /// </summary>
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepositories;

        public BeerService(IBeerRepository beerRepositories)
        {
            _beerRepositories = beerRepositories ?? throw new ArgumentNullException(nameof(_beerRepositories));
        }

        /// <inheritdoc cref="IBeerService.CreateBeer"/>
        public async Task<Beer> CreateBeer(Beer newBeer, CancellationToken cancellationToken)
        {
            var existingBeer = await _beerRepositories.GetByIdAsync(newBeer.BeerId, cancellationToken);
            if (existingBeer != null)
                throw new InvalidOperationException("Beer with this ID already exists.");

            return await _beerRepositories.AddAsync(newBeer, cancellationToken);
        }

        /// <inheritdoc cref="IBeerService.DeleteBeer"/>
        public async Task DeleteBeer(Beer beer, CancellationToken cancellationToken)
        {
            var existingBeer = await _beerRepositories.GetByIdAsync(beer.BeerId, cancellationToken);
            if (existingBeer == null)
                throw new InvalidOperationException("The specified beer was not found.");

            await _beerRepositories.RemoveAsync(beer, cancellationToken);
        }

        /// <inheritdoc cref="IBeerService.GetBeerById"/>
        public async Task<Beer> GetBeerById(int id, CancellationToken cancellationToken)
        {
            return await _beerRepositories.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc cref="IBeerService.GetBeersByBrewery"/>
        public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken)
        {
            var ListBeer = await _beerRepositories.GetBeersByBrewery(breweryId, cancellationToken);
            if (!ListBeer.Any())
                throw new InvalidOperationException("Beer with this breweryId was not found.");
            return ListBeer;
        }
    }
}
