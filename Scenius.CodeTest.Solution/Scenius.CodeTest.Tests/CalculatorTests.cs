using Scenius.CodeTest.Consumer;

namespace Scenius.CodeTest.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator = new Calculator(); 
  
    [Fact]
    public void Subtracts()
    {
        Assert.Equal(_calculator.Calc("10-1").result, 9d);
    }
    
    [Fact]
    public void Adds()
    {
        Assert.Equal(_calculator.Calc("10+1").result, 11d);
    }
    
    [Fact]
    public void Divides()
    {
        Assert.Equal(_calculator.Calc("10/2").result, 5d);
    }
    
    [Fact]
    public void Multiplies()
    {
        Assert.Equal(_calculator.Calc("10*9").result, 90d);
    }
    
    [Fact]
    public void Chains()
    {
        Assert.Equal(_calculator.Calc("10-1+10").result, 19d);
    }
    
    [Fact]
    public void UsesBrackets()
    {
        Assert.Equal(_calculator.Calc("10 * (8-1)").result, 70d);
    }
    
    [Fact]
    public void SupportsUnary()
    {
        Assert.Equal(_calculator.Calc("-(1)").result, -1d);
    }
    
    [Fact]
    public void RejectsInvalidFormula1()
    {
        Assert.Throws<Exception>(() =>
        {
            _calculator.Calc("abc");
        });
    }
    
    [Fact]
    public void RejectsInvalidFormula2()
    {
        Assert.Throws<Exception>(() =>
        {
            _calculator.Calc("a - b * c");
        });
    }
}