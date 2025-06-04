using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [StringLength(50)]
        public string Names { get; set; }
        [Required]
        [StringLength(50)]
        public string LastNames { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
