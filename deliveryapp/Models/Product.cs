using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryapp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int IdRestaurant { get; set; }
        [ForeignKey("IdRestaurant")]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
