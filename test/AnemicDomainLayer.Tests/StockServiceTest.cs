using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Xunit;

namespace AnemicDomainLayer.Tests
{
    public class StockServiceTest
    {
        DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));

        public class Constructor : StockServiceTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullContext()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new StockService(null));

                // assert
                Assert.Equal("context", exception.ParamName);
            }
        }

        public class AddStock : StockServiceTest
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
                int result = sut.AddStock(productId: 1, amount: 2);

                // assert
                Assert.Equal(3, result);
                var product = assertContext.Products.Find(1);
                Assert.Equal(3, product.QuantityInStock);
            }
        }

        public class RemoveStock : StockServiceTest
        {
            [Fact]
            public void ShouldRemoveTheSpecifiedAmountFromQuantityInStock()
            {
                // arrange
                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);
                using var assertContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 10 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // act
                int result = sut.RemoveStock(productId: 1, amount: 5);

                // assert
                Assert.Equal(5, result);
                var product = assertContext.Products.Find(1);
                Assert.Equal(5, product.QuantityInStock);
            }

            [Fact]
            public void ShouldThrowNotEnoughStockExceptionGivenAmountGreaterThanQuantityInStock()
            {
                // arrange
                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 10 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // act
                var exception = Assert.Throws<NotEnoughStockException>(() => sut.RemoveStock(1, 20));

                // assert
                Assert.Equal(20, exception.AmountToRemove);
                Assert.Equal(10, exception.QuantityInStock);
            }
        }
    }
}
