namespace RichDomainLayer
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int QuantityInStock { get; private set; }

        public Product(string name, int quantityInStock)
        {
            Name = name;
            QuantityInStock = quantityInStock;
        }
        
        public Product(int id, string name, int quantityInStock)
            : this(name, quantityInStock)
        {
            Id = id;
        }

        public void AddStock(int amount)
        {
            QuantityInStock += amount;
        }

        public void RemoveStock(int amount)
        {
            if(amount > QuantityInStock)
            {
                throw new NotEnoughStockException(quantityInStock: QuantityInStock, amountToRemove: amount);
            }
            else
            {
                QuantityInStock -= amount;
            }
        }
    }
}
