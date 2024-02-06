using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Models.Products
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public DateTime AddedOn { get; set; }
    }
}
