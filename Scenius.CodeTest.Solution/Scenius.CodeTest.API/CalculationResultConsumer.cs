using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Scenius.CodeTest.API.Hubs;
using Scenius.CodeTest.API.Models;
using Scenius.CodeTest.Contracts;

public class CalculationResultConsumer :
    IConsumer<CalculationResultContract>
{
    private readonly DataContext _context;
    private readonly IHubContext<CalculationsHub> _hub;

    public CalculationResultConsumer(DataContext context, IHubContext<CalculationsHub> hub)
    {
        _context = context;
        _hub = hub;
    }

    public async Task Consume(ConsumeContext<CalculationResultContract> context)
    {
        Console.WriteLine("received msg");
        await _hub.Clients.All.SendAsync("ReceiveMessage", context.Message);
    }
}