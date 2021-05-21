using System;

namespace CommonLibrary.Events
{
    public class ProductCheckoutEvent : IntegrationBaseEvent
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}