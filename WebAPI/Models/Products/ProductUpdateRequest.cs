using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Products
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}