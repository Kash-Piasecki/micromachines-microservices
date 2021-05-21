using System;

namespace MMStock.Data
{
        public record StockReadDto(Guid id, Guid ProductId, int Quantity);

        public record StockUpsertDto(Guid ProductId, int Quantity);
}