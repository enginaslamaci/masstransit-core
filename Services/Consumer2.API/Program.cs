using Consumer2.API.Consumers;
using Consumer2.API.Consumers.Definitions;
using Consumer2.API.Consumers.Faults;
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
    opt.AddConsumer<CompleteProcessEventConsumer2, CompleteProcessConsumer2Definition>();
    opt.AddConsumer<CompleteProcessFaultConsumer2>();

    opt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username(RabbitMQConstants.Username);
            host.Password(RabbitMQConstants.Password);
        });
        cfg.ConfigureEndpoints(context);

       // cfg.UseRateLimit(3, TimeSpan.FromSeconds(10)); // 3 request in 10 seconds
       // cfg.UseMessageRetry(r => r.Immediate(3)); // try 3 times if error

       // cfg.ReceiveEndpoint(RabbitMQConstants.CompleteProcessQuene + "-2", e =>
       //    e.ConfigureConsumer<CompleteProcessEventConsumer2>(context)
       //);
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
