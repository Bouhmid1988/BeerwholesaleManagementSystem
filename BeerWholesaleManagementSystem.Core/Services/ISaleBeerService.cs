using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;

public interface ISaleBeerService
{
    Task<SaleBeer> CreateSaleBeer(SaleBeer newSaleBeer, CancellationToken cancellationToken);
}
