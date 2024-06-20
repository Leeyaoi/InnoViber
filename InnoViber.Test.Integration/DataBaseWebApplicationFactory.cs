using InnoViber.DAL.Data;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.Test.Integration;

public class DataBaseWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    internal readonly WebApplicationFactory<Program> WebHost;

    public DataBaseWebApplicationFactory()
    {
        WebHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(i =>i.ServiceType == typeof(DbContextOptions<ViberContext>));

                services.Remove(dbContextDescriptor!);

                var publishEndpoint = services.SingleOrDefault(i => i.ServiceType == typeof(IPublishEndpoint));

                services.Remove(publishEndpoint!);

                services.AddDbContext<ViberContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbContextTest");
                });

                services.AddMassTransitTestHarness();
            }));
    }
}