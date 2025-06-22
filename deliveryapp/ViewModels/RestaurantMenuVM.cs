using deliveryapp.Models;

namespace deliveryapp.ViewModels
{
    public class RestaurantMenuVM
    {
        public Restaurant Restaurant { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
