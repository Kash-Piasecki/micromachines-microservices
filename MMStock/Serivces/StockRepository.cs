using System;
using System.Collections.Generic;
using System.Linq;
using MMStock.Data;

namespace MMStock.Serivces
{
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _db;

        public StockRepository(StockContext db)
        {
            _db = db;
        }

        public IList<Stock> GetAll()
        {
            return _db.Stock.ToList();
        }

        public Stock GetSingle(Func<Stock, bool> condition)
        {
            return _db.Stock.FirstOrDefault(condition);
        }

        public Stock Add(Stock entity)
        {
            _db.Stock.Add(entity);
            Save();
            return entity;
        }

        public Stock Edit(Stock entity)
        {
            _db.Stock.Update(entity);
            Save();
            return entity;
        }

        public void Delete(Stock entity)
        {
            _db.Stock.Remove(entity);
            Save();
        }

        public void Save() =>_db.SaveChanges();
    }
}