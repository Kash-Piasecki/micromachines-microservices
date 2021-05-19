using System;
using System.Collections.Generic;
using System.Linq;
using MMProducts.Data;

namespace MMProducts.Services
{
    class ProductsRepository : IProductsRepository
    {
        private readonly ProductsContext _db;
        
        public ProductsRepository(ProductsContext db)
        {
            _db = db;
        }
        
        public IList<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public Product GetSingle(Func<Product, bool> condition)
        {
            return _db.Products.FirstOrDefault(condition);
        }

        public Product Add(Product entity)
        {
            _db.Products.Add(entity);
            Save();
            return entity;
        }

        public Product Edit(Product entity)
        {
            _db.Products.Update(entity);
            Save();
            return entity;
        }

        public void Delete(Product entity)
        {
            _db.Products.Remove(entity);
            Save();
        }

        public void Save() => _db.SaveChanges();
    }
}