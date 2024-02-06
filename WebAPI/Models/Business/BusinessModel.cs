using WebApi.Entities;
using WebAPI.Models;

namespace WebApi.Models.Business
{
    public class BusinessModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCategory { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessLocation { get; set; }
        public string BusinessPhone { get; set; }
        public virtual List<Product> Shops { get; set; } = new List<Product>();
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; }
    }
}
