using Microsoft.AspNetCore.Builder;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReceivedService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.ConfigureServices((_, services) =>
{
    services.AddMassTransit(x =>
    {
        x.AddConsumer<MessageConsumer>();
        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host("amqp://guest:guest@localhost:5672");
            cfg.ReceiveEndpoint("message-queue", e =>
            {
                e.ConfigureConsumer<MessageConsumer>(context);
            });
        });
        
    });
    services.AddMassTransitHostedService();
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

app.Run();