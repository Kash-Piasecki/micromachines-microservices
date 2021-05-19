using System;
using System.Collections.Generic;
using System.Linq;

namespace MMBasket.Data
{
    public static class Extensions
    {
        public static BasketReadDto GetBasketReadDto(IList<Basket> baskets, IEnumerable<ProductReadDto> productReadDtos, UserReadDto userReadDto)
        {
            var productNames = baskets.Select(basket =>
            {
                var item = productReadDtos.Single(product => product.Id == basket.ProductId);
                return item.Name;
            });
            var userFullName = $"{userReadDto.FirstName} {userReadDto.LastName}";
            var price = baskets.Select(basket =>
            {
                var item = productReadDtos.Single(product => product.Id == basket.ProductId);
                return item.Price * basket.Quantity;
            }).Sum();
            return new BasketReadDto(Guid.NewGuid(), userFullName, productNames, price);
        }
    }
}