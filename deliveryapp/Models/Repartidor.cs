using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace deliveryapp.Models
{
    public class Repartidor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Nombres  { get; set; }
        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string status { get; set; } // e.g., "Available", "On Delivery", "Offline"

        public string Dni {  get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
    }
}
