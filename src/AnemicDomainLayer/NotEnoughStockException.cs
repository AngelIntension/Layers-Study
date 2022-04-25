using System;

namespace AnemicDomainLayer
{
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException(int amountToRemove, int quantityInStock)
            : base($"You cannot remove {amountToRemove} item(s) when there is only {quantityInStock} item(s).")
        {
            AmountToRemove = amountToRemove;
            QuantityInStock = quantityInStock;
        }

        public int QuantityInStock { get; set; }
        public int AmountToRemove { get; set; }
    }
}
