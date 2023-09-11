using BeerWholesaleManagementSystem.Core.Models;

namespace BeerWholesaleManagementSystem.Core.DTO;

public class QuoteResquestDto
{
    public int WholesalerId { get; set; }
    public List<QuoteBeerItemRequestDto> QuoteBeerItemDto { get; set; }
    public DateTime RequestDate { get; set; }
}
