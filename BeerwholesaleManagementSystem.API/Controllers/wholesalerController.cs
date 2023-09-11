using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Services;
using FluentValidation;
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
        private readonly IWholesalerService _wholesalerService;
        private readonly IMapper _mapper;
        private readonly IValidator<QuoteResquestDto> _quoteRequestValidator; 


        public WholesalerController(IStockService stockService, IMapper mapper, IWholesalerService wholesalerService, IValidator<QuoteResquestDto> commandRequestValidator)
        {
            _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _wholesalerService = wholesalerService ?? throw new ArgumentNullException(nameof(mapper));
            _quoteRequestValidator = commandRequestValidator ?? throw new ArgumentNullException();
        }
        /// <summary>
        /// Updates the quantity of a stock item.
        /// </summary>
        /// <param name="stockId">The ID of the stock.</param>
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
        /// <summary>
        /// Generates a quote for a wholesale Quote.
        /// </summary>
        /// <param name="quoteResquestDto">Details of the Quote.</param>
        /// <param name="cancellationToken">task cancellation token.</param>
        /// <returns>The generated quote.</returns>
        [HttpPost("generate-quote")]
        [ProducesResponseType(typeof(QuoteResponseDto), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> GenerateQuote([FromBody] QuoteResquestDto quoteResquestDto, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _quoteRequestValidator.ValidateAsync(quoteResquestDto, cancellationToken);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
                
                var quote = await _wholesalerService.GenerateQuote(quoteResquestDto, cancellationToken);
                return Ok(quote);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
