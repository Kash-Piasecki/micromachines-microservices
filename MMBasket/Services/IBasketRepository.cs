using System;
using System.Collections.Generic;
using CommonLibrary;
using MMBasket.Data;

namespace MMBasket.Services
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        IList<Basket> GetAll(Func<Basket, bool> condition);
    }
}