using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryapp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
       
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; } // Optional, if delivery is involved
        public int TotalItems { get; set; }

        [ForeignKey("UserId")]
        public Usuario User { get; set; }
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
        public int RepartidorId { get; set; }

        [ForeignKey("RepartidorId")]
        public Repartidor Repartidor { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

       
    }
}
