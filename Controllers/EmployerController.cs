using Microsoft.AspNetCore.Mvc;
using EasyEmployerBackend.Models;
using EasyEmployerBackend.Services;

namespace EasyEmployerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployerController : ControllerBase
{
    private readonly ILoketService _loketService;
    private readonly ILogger<EmployerController> _logger;

    public EmployerController(ILoketService loketService, ILogger<EmployerController> logger)
    {
        _loketService = loketService;
        _logger = logger;
    }

    /// <summary>
    /// Creates a new employer in the Loket system
    /// </summary>
    /// <param name="request">Request containing AccessToken and Werkgevernaam</param>
    /// <returns>Response with the ID of the created employer</returns>
    [HttpPost]
    public async Task<ActionResult<CreateEmployerResponse>> CreateEmployer([FromBody] CreateEmployerRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken))
            {
                return BadRequest(new { error = "AccessToken is required" });
            }

            if (string.IsNullOrWhiteSpace(request.Werkgevernaam))
            {
                return BadRequest(new { error = "Werkgevernaam is required" });
            }

            _logger.LogInformation("Received request to create employer");

            var result = await _loketService.CreateEmployerAsync(request.AccessToken, request.Werkgevernaam);

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error while creating employer");
            return StatusCode(StatusCodes.Status502BadGateway, new { error = "Failed to communicate with Loket API" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating employer");
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred" });
        }
    }
}
