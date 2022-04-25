using DataLayer;
using ForEvolve.DependencyInjection;
using ForEvolve.EntityFrameworkCore.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RichPresentationLayer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencyInjectionModules(typeof(Program).Assembly);
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
            app.Seed<ProductContext>();
        }
    }

    public class RichDomainLayerModule : DependencyInjectionModule
    {
        public RichDomainLayerModule(IServiceCollection services) : base(services)
        {
            services.AddScoped<RichDomainLayer.IStockService, RichDomainLayer.StockService>();
            services.AddScoped<RichDomainLayer.IProductService, RichDomainLayer.ProductService>();
        }
    }

    public class DataLayerModule : DependencyInjectionModule
    {
        public DataLayerModule(IServiceCollection services) : base(services)
        {
            services.AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("RichProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
            services.AddForEvolveSeeders().Scan<Startup>();
        }
    }

    public class ProductSeeder : ISeeder<ProductContext>
    {
        public void Seed(ProductContext db)
        {
            db.Products.Add(new Product
            {
                Id = 1,
                Name = "Banana",
                QuantityInStock = 50
            });
            db.Products.Add(new Product
            {
                Id = 2,
                Name = "Apple",
                QuantityInStock = 20
            });
            db.Products.Add(new Product
            {
                Id = 3,
                Name = "Habanero Pepper",
                QuantityInStock = 10
            });
            db.SaveChanges();
        }
    }
}
