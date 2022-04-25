using AnemicDomainLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnemicPresentationLayer.Controllers
{
    [ApiController]
    [Route("/products/{productId}/")]
    public class StocksController : ControllerBase
    {
        private IStockService stockService;

        public StocksController(IStockService stockService)
        {
            this.stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
        }

        [HttpPost("add-stocks")]
        public ActionResult<StockLevel> Add(int productId, [FromBody] AddStocksCommand command)
        {
            var quantityInStock = stockService.AddStock(productId, command.Amount);
            var stockLevel = new StockLevel(quantityInStock);
            return Ok(stockLevel);
        }

        [HttpPost("remove-stocks")]
        public ActionResult<StockLevel> Remove(int productId, [FromBody] RemoveStocksCommand command)
        {
            try
            {
                var quantityInStock = stockService.RemoveStock(productId, command.Amount);
                var stockLevel = new StockLevel(quantityInStock);
                return Ok(stockLevel);
            }
            catch(NotEnoughStockException ex)
            {
                return Conflict(new
                {
                    ex.Message,
                    ex.AmountToRemove,
                    ex.QuantityInStock,
                });
            }
        }

        public class StockLevel
        {
            public StockLevel(int quantityInStock)
            {
                QuantityInStock = quantityInStock;
            }

            public int QuantityInStock { get; set; }
        }

        public class AddStocksCommand
        {
            public int Amount { get; set; }
        }

        public class RemoveStocksCommand
        {
            public int Amount { get; set; }
        }
    }
}
