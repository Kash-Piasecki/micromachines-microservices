using System;
using System.Threading.Tasks;
using CommonLibrary.Events;
using MassTransit;
using MMStock.Serivces;

namespace MMStock.EventBusConsumer
{
    public class BasketUpdateConsumer : IConsumer<BasketUpdateEvent>
    {
        private readonly IStockRepository _stockRepository;

        public BasketUpdateConsumer(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task Consume(ConsumeContext<BasketUpdateEvent> context)
        {
            var basketUpdateEvent = new BasketUpdateEvent()
            {
                Id = context.Message.Id,
                CreationDate = context.Message.CreationDate,
                ProductId = context.Message.ProductId,
                Quantity = context.Message.Quantity
            };
            var stock = _stockRepository.GetSingle(x => x.ProductId == basketUpdateEvent.ProductId);
            if ((stock.Quantity - basketUpdateEvent.Quantity) >= 0)
            {
                stock.Quantity -= basketUpdateEvent.Quantity;
                _stockRepository.Edit(stock);
                _stockRepository.Save();
            }
            else
            {
                throw new Exception("Cannot get below 0 quantity in the stock");
            }
        }
    }
}