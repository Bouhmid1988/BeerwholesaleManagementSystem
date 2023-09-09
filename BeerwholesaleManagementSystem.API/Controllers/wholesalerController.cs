using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWholesaleManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing stock of a wholesaler.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public WholesalerController(IStockService stockService, IMapper mapper)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Updates the quantity of a stock item.
        /// </summary>
        /// <param name="stockId">The ID of the stock item to update.</param>
        /// <param name="newQuantity">The new quantity value to set.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>
        /// - OkResult (200): If the stock quantity was successfully updated.
        /// - NotFoundObjectResult (404): If the specified stock item was not found.
        /// </returns>
        [HttpPut("update-stock-quantity/{stockId}")]
        public async Task<ActionResult<StockDto>> UpdateStockQuantity(int stockId, [FromBody] int newQuantity, CancellationToken cancellationToken)
        {
            try
            {
                var updatedStock = await _stockService.UpdateStockQuantity(stockId, newQuantity, cancellationToken);
                var updatedStockDto = _mapper.Map<StockDto>(updatedStock);
                return Ok(updatedStockDto);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
