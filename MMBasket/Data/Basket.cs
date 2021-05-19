using System;

namespace MMBasket.Data
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsDone { get; set; }
    }
}