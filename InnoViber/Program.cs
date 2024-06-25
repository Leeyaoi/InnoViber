using InnoViber.DAL.DI;
using InnoViber.User.DAL.DI;
using InnoViber.BLL.DI;
using InnoViber.API.DI;
using InnoViber.Domain.DI;
using InnoViber.API.Extensions;

namespace InnoViber;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.RegisterDALDependencies(builder.Configuration);

        builder.Services.RegisterUserDALDependencies();

        builder.Services.RegisterBLLDependencies();

        builder.RegisterAPIDependencies();

        builder.Services.RegisterDomainDependencies();

        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BuisnessLayerDependencies).Assembly);

        var app = builder.Build();

        app.UseExeptionHandlerMiddleware();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
