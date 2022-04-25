using Microsoft.AspNetCore.Mvc;
using RichDomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RichPresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDetails>> Get()
        {
            var products = productService
                .All()
                .Select(x => new ProductDetails(
                    id: x.Id,
                    name: x.Name,
                    quantityInStock: x.QuantityInStock
                ));
            return Ok(products);
        }

        public class ProductDetails
        {
            public ProductDetails(int id, string name, int quantityInStock)
            {
                Id = id;
                Name = name ?? throw new ArgumentNullException(nameof(name));
                QuantityInStock = quantityInStock;
            }

            public int Id { get; }
            public string Name { get; }
            public int QuantityInStock { get; }
        }
    }
}
