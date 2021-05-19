using System;

namespace MMProducts.Data
{
    public static class Extensions
    {
        public static ProductReadDto AsDto(this Product product)
        {
            return new ProductReadDto(product.Id, product.Name, product.Price);
        }

        public static Product AsProduct(this ProductUpsertDto productUpsertDto)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = productUpsertDto.Name,
                Price = productUpsertDto.Price,
            };
        }

        public static Product Edit(this Product product, ProductUpsertDto productUpsertDto)
        {
            product.Name = productUpsertDto.Name;
            product.Price = productUpsertDto.Price;
            return product;
        }
    }
}