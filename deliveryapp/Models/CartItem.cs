using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryapp.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int CartId { get; set; } 
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity; 
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; } 
    }
}
