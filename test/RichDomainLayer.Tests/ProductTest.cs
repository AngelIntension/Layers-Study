using Xunit;

namespace RichDomainLayer.Tests
{
    public class ProductTest
    {
        public class AddStock : ProductTest
        {
            [Fact]
            public void ShouldAddTheSpecifiedAmountToQuantityInStock()
            {
                // arrange
                var sut = new Product("Product 1", quantityInStock: 0);

                // act
                sut.AddStock(2);

                // assert
                Assert.Equal(2, sut.QuantityInStock);
            }
        }
    }

    public class RemoveStock : ProductTest
    {
        [Fact]
        public void ShouldRemoveTheSpecifiedAmountFromQuantityInStock()
        {
            // arrange
            var sut = new Product("Product 1", quantityInStock: 5);

            // act
            sut.RemoveStock(2);

            // assert
            Assert.Equal(3, sut.QuantityInStock);
        }

        [Fact]
        public void ShouldThrow_NotEnoughStockException_GivenAmountGreaterThanQuantityInStock()
        {
            // arrange
            var sut = new Product("Product 1", quantityInStock: 5);

            // act
            var stockException = Assert.Throws<NotEnoughStockException>(() => sut.RemoveStock(6));

            // assert
            Assert.Equal(5, stockException.QuantityInStock);
            Assert.Equal(6, stockException.AmountToRemove);
        }
    }
}
