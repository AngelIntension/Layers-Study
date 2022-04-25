using AnemicDomainLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnemicPresentationLayer.Controllers
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
            var products = productService.All().Select(p => new ProductDetails(p.Id, p.Name, p.QuantityInStock));
            return Ok(products);
        }

        public class ProductDetails
        {
            public ProductDetails(int id, string name, int quantityInStock)
            {
                Id = id;
                Name = name;
                QuantityInStock = quantityInStock;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public int QuantityInStock { get; set; }
        }
    }
}
