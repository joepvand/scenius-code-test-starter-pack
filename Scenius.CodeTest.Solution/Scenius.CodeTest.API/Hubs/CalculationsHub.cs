using Microsoft.AspNetCore.SignalR;
using Scenius.CodeTest.API.Models;

namespace Scenius.CodeTest.API.Hubs;

public class CalculationsHub :Hub
{
    public async Task SendMessage(CalculationResult result)
    {
        Console.WriteLine("got message"+result.calculation);
        await Clients.All.SendAsync("ReceiveMessage", result);
        Console.WriteLine("sent");
    }
}