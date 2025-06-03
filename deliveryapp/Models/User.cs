using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace deliveryapp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Names { get; set; }
        [Required]
        [StringLength(50)]
        public string LastNames { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Roles Rol { get; set; }
    }
}
