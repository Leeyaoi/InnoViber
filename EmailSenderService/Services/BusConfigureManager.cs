using Microsoft.Extensions.Configuration;
using EmailSenderService.Interfaces;
using MassTransit;
using SharedModels;

namespace EmailSenderService.Services;

public class BusConfigureManager : IBusConfigureManager
{
    private readonly IEmailSenderService _emailSender;
    private readonly IConfiguration _configuration;

    public BusConfigureManager(IEmailSenderService emailSender, IConfiguration configuration)
    {
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public IBusControl SetUpBus()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(_configuration["RabbitMq:HostName"], _configuration["RabbitMq:VirtualHost"], h =>
            {
                h.Username(_configuration["RabbitMq:UserName"]);
                h.Password(_configuration["RabbitMq:Passkey"]);
            });

            cfg.ReceiveEndpoint(_configuration["RabbitMq:QueueName"], ep =>
            {
                ep.Handler<IUserInfo>(context => _emailSender.SendEmailAsync(context.Message));
            });
        });

        return bus;
    }
}