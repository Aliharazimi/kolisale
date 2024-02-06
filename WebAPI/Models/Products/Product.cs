using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Business;
using WebAPI.Models.Products;

namespace WebAPI.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int UserId { get; set; }
        public int BusinessId { get; set; }
        public BusinessModel BusinessModel { get; set; }
        public string ProductDescription { get; set; }
        public ProductCategory Category { get; set; }

    }
}