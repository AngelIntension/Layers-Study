using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RichDomainLayer
{
    public class ProductService : IProductService
    {
        private readonly ProductContext context;

        public ProductService(ProductContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Product> All()
        {
            return context.Products.Select(p => new Product(p.Id, p.Name, p.QuantityInStock));
        }
    }
}
