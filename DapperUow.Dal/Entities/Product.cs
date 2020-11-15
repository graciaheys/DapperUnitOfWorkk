using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUow.Dal.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal  Price { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
    }
}
