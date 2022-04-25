using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Xunit;

namespace RichDomainLayer.Tests
{
    public class StockServiceTests
    {
        private readonly DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        public class AddStock : StockServiceTests
        {
            [Fact]
            public void ShouldAddTheSpecifiedAmountToQuantityInStock()
            {
                // arrange
                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);
                using var assertContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 1 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // act
                int quantityInStock = sut.AddStock(productId: 1, amount: 2);

                // assert
                Assert.Equal(3, quantityInStock);
                var actual = assertContext.Products.Find(1);
                Assert.Equal(3, actual.QuantityInStock);
            }
        }

        public class RemoveStock : StockServiceTests
        {
            [Fact]
            public void ShouldRemoveTheSpecifiedAmountFromQuantityInStock()
            {
                // arrange
                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);
                using var assertContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 3 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // act
                int quantityInStock = sut.RemoveStock(productId: 1, amount: 2);

                // assert
                Assert.Equal(1, quantityInStock);
                var product = assertContext.Products.Find(1);
                Assert.Equal(1, product.QuantityInStock);
            }
        }
    }
}
