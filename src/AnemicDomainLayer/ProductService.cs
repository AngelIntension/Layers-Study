using DataLayer;
using System.Collections.Generic;
using System.Linq;

namespace AnemicDomainLayer
{
    public class ProductService
    {
        private ProductContext context;

        public ProductService(ProductContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public IEnumerable<Product> All()
        {
            var products = context.Products
                .Select(p => new Product() { 
                    Name = p.Name,
                });
            return products;
        }
    }
}
