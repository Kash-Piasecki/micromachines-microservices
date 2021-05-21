using System;
using System.Threading.Tasks;
using CommonLibrary.Events;
using MassTransit;
using MMStock.Data;
using MMStock.Serivces;

namespace MMStock.EventBusConsumer
{
    public class ProductCreationConsumer : IConsumer<ProductCreationEvent>
    {
        private readonly IStockRepository _stockRepository;

        public ProductCreationConsumer(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Consume(ConsumeContext<ProductCreationEvent> context)
        {
            var productCreationEvent = new ProductCreationEvent()
            {
                Id = context.Message.Id,
                CreationDate = context.Message.CreationDate,
                ProductId = context.Message.ProductId,
                Name = context.Message.Name,
                Price = context.Message.Price
            };
            var stock = new Stock()
            {
                Id = Guid.NewGuid(),
                ProductId = productCreationEvent.ProductId,
                Quantity = 10,
            };
            _stockRepository.Add(stock);
        }
    }
}