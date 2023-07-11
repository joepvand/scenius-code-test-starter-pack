using MassTransit;
using Scenius.CodeTest.Contracts;

namespace Scenius.CodeTest.API.Publishers;

public class CalculationResultPublisher
{
    private readonly IPublishEndpoint _bus;

    public CalculationResultPublisher(IPublishEndpoint bus)
    {
        this._bus = bus;
    }

    public async  Task SendCalculationResultAsync(CalculationResults res)
    {
        await _bus.Publish(new CalculationResultContract(  res.operation, res.result));
    }
}