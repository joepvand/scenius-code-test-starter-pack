using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.Contracts;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<CalculationResultPublisher>();
builder.Services.AddMassTransit(x =>
{
    // elided...
    x.AddConsumer<CalculationConsumer>();
    x.UsingRabbitMq((context,cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});
using IHost host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

await host.RunAsync();