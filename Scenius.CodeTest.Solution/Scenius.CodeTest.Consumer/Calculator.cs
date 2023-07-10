using System.Data;
using Scenius.CodeTest.Contracts.Models;

namespace Scenius.CodeTest.Consumer;

public class Calculator
{
    public CalculationResults Calc(string formula)
    {
        DataTable dt = new DataTable();
        object result = dt.Compute(formula, "");

        double resultAsDouble = double.Parse(result.ToString());
        return new CalculationResults(Guid.NewGuid(), formula, resultAsDouble);
    }
}