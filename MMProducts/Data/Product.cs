using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMProducts.Data
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(5, 2)")] 
        public decimal Price { get; set; }
    }
}