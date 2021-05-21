using System;

namespace CommonLibrary.Events
{
    public class BasketUpdateEvent : IntegrationBaseEvent
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}