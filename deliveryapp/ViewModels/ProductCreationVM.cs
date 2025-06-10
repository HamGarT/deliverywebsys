using deliveryapp.Models;

namespace deliveryapp.ViewModels
{
    public class ProductCreationVM
    {
        public Product Product  { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public IFormFile ImageUpload { get; set; }
        public int SelectedRestaurantId { get; set; }
    }
}
