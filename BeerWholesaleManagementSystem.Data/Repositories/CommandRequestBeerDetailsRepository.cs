using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;

namespace BeerWholesaleManagementSystem.Data.Repositories;

/// <summary>
/// Repository for managing Command Request Beer Details.
/// </summary>
public class CommandRequestBeerDetailsRepository : Repository<QuoteRequestBeerDetail>, IQuoteRequestBeerDetailsRepository
{
    private readonly BeerDbContext _beerDbContext;

    public CommandRequestBeerDetailsRepository(BeerDbContext context) : base(context)
    {
        _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
}
