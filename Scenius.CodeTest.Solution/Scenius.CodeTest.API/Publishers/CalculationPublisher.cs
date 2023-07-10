using MassTransit;
using Scenius.CodeTest.Contracts;

namespace Scenius.CodeTest.API.Publishers;

public class CalculationPublisher
{
    private readonly IPublishEndpoint _bus;

    public CalculationPublisher(IPublishEndpoint bus)
    {
        this._bus = bus;
    }

    public async  Task SendCalculationAsync(string value)
    {
        await _bus.Publish(new CalculationContract() { temp = value });
    }
}