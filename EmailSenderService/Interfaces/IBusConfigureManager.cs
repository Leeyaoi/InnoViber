using MassTransit;

namespace EmailSenderService.Interfaces;

public interface IBusConfigureManager
{
    public IBusControl SetUpBus();
}