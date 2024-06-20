using EmailSenderService.Interfaces;
using EmailSenderService.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IIntegrationServiceSmtpClient, IntegrationServiceSmtpClient>();
builder.Services.AddSingleton<IEmailSenderService, EmailSender>();
builder.Services.AddSingleton<IBusConfigureManager, BusConfigureManager>();

var services = builder.Build().Services;

var bus = services.GetService<IBusConfigureManager>()!.SetUpBus();

await bus.StartAsync();

while (Console.ReadKey().Key != ConsoleKey.Q) { }

await bus.StopAsync();