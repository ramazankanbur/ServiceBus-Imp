using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Common;
using ServiceBusProducer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ServiceBusAdministrationClient>(provider => new ServiceBusAdministrationClient(Constants.SERVICEBUSCONNSTRING));
builder.Services.AddSingleton<ServiceBusClient>(provider => new ServiceBusClient(Constants.SERVICEBUSCONNSTRING));
builder.Services.AddSingleton<AzureServiceBusAdministration>(provider => new AzureServiceBusAdministration(provider.GetRequiredService<ServiceBusAdministrationClient>()));
builder.Services.AddSingleton<AzureServiceBusMessageManegement>(provider => new AzureServiceBusMessageManegement(provider.GetRequiredService<ServiceBusClient>()));



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

