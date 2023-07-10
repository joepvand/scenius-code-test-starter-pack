using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Scenius.CodeTest.Consumer;
using Scenius.CodeTest.Contracts;

public class CalculationConsumer :
    IConsumer<CalculationContract>
{
    readonly ILogger<CalculationConsumer> _logger;
    private readonly DataContext _context;
    private readonly Calculator _calculator;
    public CalculationConsumer(ILogger<CalculationConsumer> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
        _calculator = new Calculator();
    }

    public Task Consume(ConsumeContext<CalculationContract> context)
    {
        _logger.LogInformation("Received Text: {Text}", context.Message.temp);
        var result = _calculator.Calc(context.Message.temp);

        _context.CalculationResults.Add(result); 
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}