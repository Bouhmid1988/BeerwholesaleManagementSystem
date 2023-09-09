using AutoMapper;
using BeerWholesaleManagementSystem.Core.DTO;
using BeerWholesaleManagementSystem.Core.Models;
using BeerWholesaleManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BeerWholesaleManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BreweryController : ControllerBase
{
    private readonly IBeerService _beerService ; 
    private readonly IMapper _mapperService;
    private readonly ILogger _logger;

    public BreweryController(IBeerService beerService, IMapper mapperService, ILogger<BreweryController> logger)
    {
        _beerService = beerService ?? throw new ArgumentNullException(nameof(_beerService));
        _mapperService = mapperService ?? throw new ArgumentNullException(nameof(_mapperService));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    /// <summary>
    /// Gets a list of beers by brewery ID.
    /// </summary>
    /// <param name="id">The ID of the brewery.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>
    /// - OkResult (200): If beers are found for the brewery.
    /// - NotFoundResult (404): If no beers are found for the brewery.
    /// - ObjectResult (500): If an error occurs during the operation.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeersByBrewery(int id, CancellationToken cancellationToken)
    {
        try
        {
            var beers = await _beerService.GetBeersByBrewery(id, cancellationToken);
            if (beers.Count() == 0)
                return NotFound($"No beer found for brewery with ID {id}");

            var listeBeerDto = _mapperService.Map<List<BeerDto>>(beers);

            return Ok(listeBeerDto);
        }
        catch (InvalidOperationException )
        {
            return NotFound($"No beer found for brewery with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving beers.");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    /// <summary>
    /// Creates a new beer.
    /// </summary>
    /// <param name="saveBeer">The beer data to create.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>
    /// - OkResult (200): If the beer was successfully created.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<BeerDto>> CreateBeer(BeerDto saveBeer, CancellationToken cancellationToken)
    {
        try
        {
            var beer = _mapperService.Map<Beer>(saveBeer);
            var newBeer = await _beerService.CreateBeer(beer, cancellationToken);
            var newBeerSave = _mapperService.Map<BeerDto>(newBeer);
            return Ok(newBeerSave);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a beer.");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    /// <summary>
    /// Deletes an entity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>
    /// - NoContentResult (204): If the entity was successfully deleted.
    /// - NotFoundResult (404): If the entity with the specified ID does not exist.
    /// - StatusCodeResult (500): If an error occurred while processing the request.
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBeerById(int id, CancellationToken cancellationToken)
    {
        var entity = await _beerService.GetBeerById(id, cancellationToken);

        if (entity == null)
            return NotFound($"Entity with ID {id} not found.");

        try
        {
            await _beerService.DeleteBeer(entity, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while Deleting a beer.");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}
