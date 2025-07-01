using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class RestaurantVM
    {
        public int  Id { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Descripción es requerida")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Dirección es requerida")]
        public string Address { get; set; }
        [RegularExpression(@"^9\d{8}$", ErrorMessage = "El número debe tener 9 dígitos y comenzar con 9.")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile ? ImageUpload { get; set; }

        public string ? ImagePath { get; set; }
    }
}
