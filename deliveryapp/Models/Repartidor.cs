using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace deliveryapp.Models
{
    public class Repartidor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombres  { get; set; }
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string status { get; set; } // e.g., "Available", "On Delivery", "Offline"

        public string Dni {  get; set; }

        public string? ImageUrl { get; set; } // Optional, if you want to store an image URL for the delivery person



    }
}
