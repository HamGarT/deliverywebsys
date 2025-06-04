﻿using System.ComponentModel.DataAnnotations;

namespace deliveryapp.ViewModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
