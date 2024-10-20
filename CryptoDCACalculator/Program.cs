using CryptoDCACalculator.Business.Services.Abstractions;
using CryptoDCACalculator.Business.Services;
using CryptoDCACalculator.Components;
using CryptoDCACalculator.DataAccess.Context;
using CryptoDCACalculator.DataAccess.Repositories.Abstractions;
using CryptoDCACalculator.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace CryptoDCACalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<CryptoDCACalculatorContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CryptoDCACalculator")));

            builder.Services.AddMudServices();

            builder.Services.AddScoped<IHistoricalPriceRepository, HistoricalPriceRepository>();

            builder.Services.AddScoped<IHistoricalPriceService, HistoricalPriceService>();

            builder.Services.AddScoped<ICryptoApiService, CryptoApiService>();

            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
