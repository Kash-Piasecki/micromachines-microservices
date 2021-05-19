using System;
using System.Collections.Generic;
using System.Linq;
using MMBasket.Data;

namespace MMBasket.Services
{
    class BasketRepository : IBasketRepository
    {
        private readonly BasketContext _db;

        public BasketRepository(BasketContext db)
        {
            _db = db;
        }

        public IList<Basket> GetAll()
        {
            return _db.Baskets.ToList();
        }
        
        public IList<Basket> GetAll(Func<Basket, bool> condition)
        {
            return _db.Baskets.Where(condition).ToList();
        }

        public Basket GetSingle(Func<Basket, bool> condition)
        {
            return _db.Baskets.FirstOrDefault(condition);
        }

        public Basket Add(Basket entity)
        {
            _db.Baskets.Add(entity);
            Save();
            return entity;
        }

        public Basket Edit(Basket entity)
        {
            _db.Baskets.Update(entity);
            Save();
            return entity;
        }

        public void Delete(Basket entity)
        {
            _db.Baskets.Remove(entity);
            Save();
        }

        public void Save() =>_db.SaveChanges();
    }
}