using DataLayer;
using System;

namespace AnemicDomainLayer
{
    public class StockService
    {
        private readonly ProductContext context;

        public StockService(ProductContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int AddStock(int productId, int amount)
        {
            var product = context.Products.Find(productId);
            product.QuantityInStock += amount;
            context.SaveChanges();
            return product.QuantityInStock;
        }

        public int RemoveStock(int productId, int amount)
        {
            var product = context.Products.Find(productId);
            if(product.QuantityInStock < amount)
            {
                throw new NotEnoughStockException(amountToRemove: amount, quantityInStock: product.QuantityInStock);
            }
            product.QuantityInStock -= amount;
            context.SaveChanges();
            return product.QuantityInStock;
        }
    }
}
