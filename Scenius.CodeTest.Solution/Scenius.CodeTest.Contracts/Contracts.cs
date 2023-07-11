namespace Scenius.CodeTest.Contracts;

public record CalculationContract(string calculation);

public record CalculationResultContract(string calculation, double result);
    public record CalculationResults(Guid id, string operation, double result);