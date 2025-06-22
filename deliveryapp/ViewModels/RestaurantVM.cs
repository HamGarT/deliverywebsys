using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class RestaurantVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile ImageUpload { get; set; }

        public string ImagePath { get; set; }
    }
}
