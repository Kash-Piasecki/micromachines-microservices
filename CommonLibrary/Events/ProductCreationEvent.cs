using System;

namespace CommonLibrary.Events
{
    public class ProductCreationEvent : IntegrationBaseEvent
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}