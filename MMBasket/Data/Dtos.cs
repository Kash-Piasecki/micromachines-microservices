using System;
using System.Collections.Generic;

namespace MMBasket.Data
{
    public record BasketReadDto(Guid Id, string UserFullName, IEnumerable<String> ProductList, decimal Price);

    public record BasketCreateDto(Guid UserId, Guid ProductId, int Quantity);

    public record ProductReadDto(Guid Id, string Name, decimal Price);

    public record UserReadDto(Guid Id, string FirstName, string LastName);
}