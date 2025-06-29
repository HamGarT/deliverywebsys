using deliveryapp.Models;

namespace deliveryapp.ViewModels
{
    public class OrdersVM
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
