using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories
{
    public class BeerRepositories : Repositories<Beer>, IBeerRepositories
    {
        private readonly BeerDbContext _beerDbContext;

        public BeerRepositories(BeerDbContext context) : base(context)
        {
            _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Beer>> GetBeersByBrewery(int breweryId, CancellationToken cancellationToken)
        {
            return await _beerDbContext.Beers
                .Where(b => b.BreweryId == breweryId)
                .ToListAsync(cancellationToken);
        }
    }
}
