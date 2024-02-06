using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Products
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProductCategoryId { get; set; }
        public int BusinessId { get; set; }
    }
}