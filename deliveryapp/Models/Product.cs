using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace deliveryapp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string  Description { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public string ?  ImageUrl { get; set; }

        public int IdRestaurant { get; set; }
        [ForeignKey("IdRestaurant")]
        public Restaurant ?  Restaurant { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
