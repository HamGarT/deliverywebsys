namespace deliveryapp.ViewModels
{
    public class RepartidorVM
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string status { get; set; } // e.g., "Available", "On Delivery", "Offline"

        public string Dni { get; set; }

        public IFormFile ImageUpload { get; set; }

        public string ImagePath { get; set; }
    }
}
