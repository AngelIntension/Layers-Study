using System;

namespace RichDomainLayer
{
    public class NotEnoughStockException : Exception
    {
        public int QuantityInStock { get; set; }
        public int AmountToRemove { get; set; }

        public NotEnoughStockException(int quantityInStock, int amountToRemove)
            : base($"You cannot remove {amountToRemove} item(s) when there is only {quantityInStock} item(s) left.")
        {
            QuantityInStock = quantityInStock;
            AmountToRemove = amountToRemove;
        }
    }
}
