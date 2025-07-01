using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class RepartidorVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Los apellidos son requeridos")]
        public string Apellidos { get; set; }

        [RegularExpression(@"^9\d{8}$", ErrorMessage = "El número debe tener 9 dígitos y comenzar con 9.")]
        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Telefono { get; set; }

        public string status { get; set; } // e.g., "Available", "On Delivery", "Offline"

        [Required(ErrorMessage = "El DNI es requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 dígitos.")]
        public string Dni { get; set; }

        public IFormFile ? ImageUpload { get; set; }

        public string ? ImagePath { get; set; }
    }
}
