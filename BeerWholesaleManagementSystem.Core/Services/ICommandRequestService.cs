using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Repositories;

namespace BeerWholesaleManagementSystem.Core.Services;

public interface ICommandRequestService : IRepository<QuoteRequest>
{
}
