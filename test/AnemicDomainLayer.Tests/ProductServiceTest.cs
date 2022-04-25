using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using Xunit;

namespace AnemicDomainLayer.Tests
{
    public class ProductServiceTest
    {
        public class Constructor : ProductServiceTest
        {
            [Fact]
            public void ShouldThrowArgumentNullErrorGivenNullContext()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new ProductService(null));

                // assert
                Assert.Equal("context", exception.ParamName);
            }
        }

        public class All : ProductServiceTest
        {
            [Fact]
            public void ShouldReturnAllProducts()
            {
                // arrange
                var builder = new DbContextOptionsBuilder<ProductContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1" });
                arrangeContext.Products.Add(new() { Name = "Product 2" });
                arrangeContext.SaveChanges();

                var sut = new ProductService(actContext);

                // act
                IEnumerable<AnemicDomainLayer.Product> result = sut.All();

                // assert
                Assert.Collection(result,
                    product => Assert.Equal("Product 1", product.Name),
                    product => Assert.Equal("Product 2", product.Name)    
                );
            }
        }
    }
}
