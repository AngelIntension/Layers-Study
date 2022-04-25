using DataLayer;

namespace RichDomainLayer
{
    public class StockService
    {
        private ProductContext context;

        public StockService(ProductContext context)
        {
            this.context = context;
        }

        public int AddStock(int productId, int amount)
        {
            var data = context.Products.Find(productId);
            var product = new Product(
                id: data.Id,
                name: data.Name,
                quantityInStock: data.QuantityInStock
            );
            product.AddStock(amount);
            data.QuantityInStock = product.QuantityInStock;
            context.SaveChanges();
            return product.QuantityInStock;
        }

        public int RemoveStock(int productId, int amount)
        {
            var data = context.Products.Find(productId);
            var product = new Product(
                id: data.Id,
                name: data.Name,
                quantityInStock: data.QuantityInStock
            );
            product.RemoveStock(amount);
            data.QuantityInStock = product.QuantityInStock;
            context.SaveChanges();
            return product.QuantityInStock;
        }
    }
}
