using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWholesaleManagementSystem.API.Controllers
{
    /// <summary>
    /// Controller for managing sale of beer.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SaleBeerController : ControllerBase
    {
        private readonly ISaleBeerService _saleBeerService;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleBeerController> _logger;

        public SaleBeerController(ISaleBeerService saleBeerService, IMapper mapper, ILogger<SaleBeerController> logger)
        {
            _saleBeerService = saleBeerService ?? throw new ArgumentNullException(nameof(saleBeerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new sale of beer.
        /// </summary>
        /// <param name="newSaleBeerDto">The data for the new sale.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>
        /// - CreatedAtActionResult (201): If the sale of beer was successfully created.
        /// - BadRequestObjectResult (400): If the request data is invalid or the sale cannot be created.
        /// - ObjectResult (500): If an error occurs during the operation.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<SaleBeerDto>> CreateSaleBeer([FromBody] SaleBeerDto newSaleBeerDto, CancellationToken cancellationToken)
        {
            try
            {
                var newSaleBeer = _mapper.Map<SaleBeer>(newSaleBeerDto);
                var createdSaleBeer = await _saleBeerService.CreateSaleBeer(newSaleBeer, cancellationToken);
                var createdSaleBeerDto = _mapper.Map<SaleBeerDto>(createdSaleBeer);
                return CreatedAtAction(nameof(CreateSaleBeer), new { id = createdSaleBeerDto.BeerId }, createdSaleBeerDto);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "An error occurred while creating a sale.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
