using MassTransit;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.API.Services;
using Scenius.CodeTest.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Rabbitmq
builder.Services.AddScoped<CalculationPublisher>();
builder.Services.AddScoped<CalculationService>();
builder.Services.AddMassTransit(x =>
{
	
	x.UsingRabbitMq((context, cfg) =>
	{
		// TOOD: Change to env vars.
		// Default port = 5672
		cfg.Host("localhost", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ConfigureEndpoints(context);
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<DataContext>();
	context.Database.EnsureCreated();
	// DbInitializer.Initialize(context);
}
app.Run();