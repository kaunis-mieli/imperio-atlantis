using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;
using FibonacciCalculator;

namespace FibonacciAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly ILogger<FibonacciController> _logger;

    public FibonacciController(ILogger<FibonacciController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{n}")]
    public async Task<ActionResult> Fibonacci(int n)
    {
        try
        {
            var result = await Calculator.Calculate(n)
                .ConfigureAwait(false);

            return Ok(result.ToString());
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest();
        }
    }
}
