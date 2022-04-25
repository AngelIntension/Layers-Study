using DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace AnemicDomainLayer
{
    public class ProductService : IProductService
    {
        private readonly ProductContext context;

        public ProductService(ProductContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public IEnumerable<Product> All()
        {
            var products = context.Products
                .Select(p => new Product()
                {
                    Id = p.Id,
                    Name = p.Name,
                    QuantityInStock = p.QuantityInStock,
                });
            return products;
        }
    }
}
