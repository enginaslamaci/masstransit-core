using Consumer.API.Consumers;
using Consumer.API.Consumers.Definitions;
using Consumer.API.Consumers.Faults;
using MassTransit;
using Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(opt =>
{
    opt.AddConsumer<CompleteProcessEventConsumer, CompleteProcessConsumerDefinition>();
    opt.AddConsumer<CompleteProcessFaultConsumer>();

    opt.AddConsumer<UpdateProcessCommandConsumer, UpdateProcessConsumerDefinition>();
    opt.AddConsumer<UpdateProcessFaultConsumer>();

    opt.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
            {
                host.Username(RabbitMQConstants.Username);
                host.Password(RabbitMQConstants.Password);
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

app.Run();
