using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.API.Services;


namespace Scenius.CodeTest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalculationsController : ControllerBase
{
	private readonly ILogger<CalculationsController> _logger;
	private readonly CalculationService _calculationService;

	public CalculationsController(ILogger<CalculationsController> logger, CalculationService calculationService)
	{
		_logger = logger;
		_calculationService = calculationService;
	}

	[HttpPost]
	public async Task<ActionResult> Post([FromBody] CalculationSubmission body)
	{
		await _calculationService.SubmitCalculation(body.calculation);
		return Ok();
	}
	
	[HttpGet]
	public async Task<List<CalculationResult>> GetAll()
	{
		var results =  await _calculationService.GetCompletedCalculations();
		
		return results.Select(x => new CalculationResult(x.operation, x.result)).ToList();
	}
}