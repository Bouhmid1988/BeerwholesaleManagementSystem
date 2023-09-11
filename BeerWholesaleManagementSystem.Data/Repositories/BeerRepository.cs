using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories
{
    public class BeerRepository : Repository<Beer>, IBeerRepository
    {
        private readonly BeerDbContext _beerDbContext;

        /// <summary>
        /// Initializes a new instance of the BeerRepository class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided context is null.</exception>
        public BeerRepository(BeerDbContext context) : base(context)
        {
            _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IBeerRepository.GetBeersByBrewery"/>
        public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken)
        {
            return await _beerDbContext.Beers
                .Where(b => b.BreweryId == breweryId)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc cref="IBeerRepository.GetBeerPriceAsync"/>
        public async Task<decimal> GetBeerPriceAsync(Beer beer, CancellationToken cancellationToken)
        {
            var price = await _beerDbContext.Beers
                .Where(b => b.BeerId == beer.BeerId)
                .Select(b => b.Price)
                .FirstOrDefaultAsync(cancellationToken);

            if (price <= 0)
            {
                throw new Exception("Beer price not found or invalid.");
            }

            return price;
        }
    }
}
