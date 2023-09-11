using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.Services;

/// <summary>
/// Define the Sale Beer Service Interface
/// </summary>
public interface ISaleBeerService
{
    Task<SaleBeer> CreateSaleBeer(SaleBeer newSaleBeer, CancellationToken cancellationToken);
}
