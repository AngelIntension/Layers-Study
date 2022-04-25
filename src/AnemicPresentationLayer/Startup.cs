using AnemicDomainLayer;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnemicPresentationLayer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataLayer();
            services.AddDomainLayer();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.SeedDataLayer();
        }
    }

    internal static class DomainLayerModule
    {
        internal static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
            return services;
        }
    }

    internal static class DataLayerModule
    {
        internal static IServiceCollection AddDataLayer(this IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("AnemicProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
            return services;
        }

        internal static IApplicationBuilder SeedDataLayer(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using(var scope = serviceScopeFactory.CreateScope())
            {
                var productContext = scope.ServiceProvider.GetRequiredService<ProductContext>();
                productContext.Products.Add(new()
                {
                    Id = 1,
                    Name = "Banana",
                    QuantityInStock = 50
                });
                productContext.Products.Add(new()
                {
                    Id = 2,
                    Name = "Apple",
                    QuantityInStock = 20
                });
                productContext.Products.Add(new()
                {
                    Id = 3,
                    Name = "Habanero Pepper",
                    QuantityInStock = 10
                });
                productContext.SaveChanges();
            }
            return app;
        }
    }
}
