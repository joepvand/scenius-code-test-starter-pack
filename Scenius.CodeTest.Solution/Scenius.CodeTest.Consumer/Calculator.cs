using System.Data;
using Scenius.CodeTest.Contracts;

namespace Scenius.CodeTest.Consumer;

public class Calculator
{
    public CalculationResults Calc(string formula)
    {
        if (!isValidExpression(formula))
        {
            throw new Exception("Input string is not in the correct format!");
        }
        DataTable dt = new DataTable();
        object result = dt.Compute(formula, "");

        double resultAsDouble = double.Parse(result.ToString());
        return new CalculationResults(Guid.NewGuid(), formula, resultAsDouble);
    }

    private bool isValidExpression(string input)
    {
        foreach (var c in input)
        {
            if (c != '(' &&
                c != ')' &&
                c != '-' &&
                c != '/' &&
                c != '+' &&
                c != '*' &&
                c != ' ' &&
                  !char.IsNumber(c))
            {
                return false;
            }
        }

        return true;
    }
}