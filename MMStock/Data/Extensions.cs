using System;

namespace MMStock.Data
{
    public static class Extensions
    {
        public static StockReadDto AsDto(this Stock stock)
        {
            return new StockReadDto(stock.Id, stock.ProductId, stock.Quantity);
        }

        public static Stock AsStock(this StockUpsertDto stockUpsertDto)
        {
            return new Stock()
            {
                Id = Guid.NewGuid(),
                ProductId = stockUpsertDto.ProductId,
                Quantity = stockUpsertDto.Quantity
            };
        }

        public static Stock Edit(this Stock stock, StockUpsertDto stockUpsertDto)
        {
            stock.ProductId = stockUpsertDto.ProductId;
            stock.Quantity = stockUpsertDto.Quantity;
            return stock;
        }
    }
}