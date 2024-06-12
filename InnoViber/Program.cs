using InnoViber.DAL.DI;
using InnoViber.BLL.DI;
using InnoViber.API.DI;
using InnoViber.Domain.DI;
using InnoViber.API.Extensions;

namespace InnoViber
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.RegisterDALDependencies(builder.Configuration);

            builder.Services.RegisterBLLDependencies();

            builder.RegisterAPIDependencies();

            builder.Services.RegisterDomainDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BuisnessLayerDependencies).Assembly);

            var app = builder.Build();

            app.UseExeptionHandlerMiddleware();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
}
