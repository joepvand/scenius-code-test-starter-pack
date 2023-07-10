namespace Scenius.CodeTest.API.Models;

public record CalculationSubmission (string calculation);
public record CalculationResult (string calculation, double result);