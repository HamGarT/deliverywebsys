using deliveryapp.Models;
using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class ProductCreationVM
    {
        public Product Product { get; set; } = new Product(); // Initialize to avoid null reference

        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile ImageUpload { get; set; }

        [Required(ErrorMessage = "Please select a restaurant")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid restaurant")]
        public int SelectedRestaurantId { get; set; }

        //[Required(ErrorMessage = "Product name is required")]
        //[MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Description is required")]
        //public string Description { get; set; }

        //[Required(ErrorMessage = "Price is required")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        //public decimal Price { get; set; }

        //public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        //[Required(ErrorMessage = "Please upload an image")]
        //public IFormFile ImageUpload { get; set; }

        //[Required(ErrorMessage = "Please select a restaurant")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please select a valid restaurant")]
        //public int SelectedRestaurantId { get; set; }

        //public string ImageUrl { get; set; }
    }
}
