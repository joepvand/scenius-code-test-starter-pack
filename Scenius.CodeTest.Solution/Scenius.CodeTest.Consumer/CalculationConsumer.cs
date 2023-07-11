using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.Consumer;
using Scenius.CodeTest.Contracts;

public class CalculationConsumer :
    IConsumer<CalculationContract>
{
    readonly ILogger<CalculationConsumer> _logger;
    private readonly DataContext _context;
    private readonly CalculationResultPublisher _publisher;
    private readonly Calculator _calculator;
    public CalculationConsumer(ILogger<CalculationConsumer> logger, DataContext context, CalculationResultPublisher publisher)
    {
        _logger = logger;
        _context = context;
        _publisher = publisher;
        _calculator = new Calculator();
    }

    public Task Consume(ConsumeContext<CalculationContract> context)
    {
        var result = _calculator.Calc(context.Message.calculation);

        _context.CalculationResults.Add(result); 
        _context.SaveChanges();

        _publisher.SendCalculationResultAsync(result);
        return Task.CompletedTask;
    }
}