using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.API.Services;
using Scenius.CodeTest.Contracts.Models;

namespace Scenius.CodeTest.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculationController : ControllerBase
{
	private readonly ILogger<CalculationController> _logger;
	private readonly CalculationService _calculationService;

	public CalculationController(ILogger<CalculationController> logger, CalculationService calculationService)
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