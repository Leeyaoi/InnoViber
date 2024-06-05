using InnoViber.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoViber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ViberContext>(option =>
            {
                //option.UseNpgsql("host=localhost;port=5432;dbname=viberDB;user=postgres;password=26088062;connect_timeout=10;sslmode=prefer");
                option.UseNpgsql("Host=localhost;Port=5432;Database=viberDB;Username=postgres;Password=26088062");
            });

            var app = builder.Build();

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
