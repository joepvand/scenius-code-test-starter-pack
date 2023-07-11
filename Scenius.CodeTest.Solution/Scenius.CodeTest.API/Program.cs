using MassTransit;
using Scenius.CodeTest.API.Hubs;
using Scenius.CodeTest.API.Publishers;
using Scenius.CodeTest.API.Services;
using Scenius.CodeTest.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>();

// Add services to the container.
// Default Policy

builder.Services.AddCors(options =>
{
	
		options.AddPolicy("AllowAll", builder =>
		{
			builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
	
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Rabbitmq
builder.Services.AddScoped<CalculationPublisher>();
builder.Services.AddScoped<CalculationService>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<CalculationResultConsumer>();
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

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CalculationsHub>("/calculationsHub");
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<DataContext>();
	context.Database.EnsureCreated();
	// DbInitializer.Initialize(context);
}
app.Run();