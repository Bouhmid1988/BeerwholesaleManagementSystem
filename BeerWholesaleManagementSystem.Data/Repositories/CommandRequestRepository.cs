using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;

namespace BeerWholesaleManagementSystem.Data.Repositories;

/// <summary>
/// Repository for managing Command Requests.
/// </summary>
public class CommandRequestRepository : Repository<QuoteRequest>, IQuoteRequestRepository
{
    private readonly BeerDbContext _beerDbContext;
    public CommandRequestRepository(BeerDbContext context) : base(context)
    {
        _beerDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }
}
