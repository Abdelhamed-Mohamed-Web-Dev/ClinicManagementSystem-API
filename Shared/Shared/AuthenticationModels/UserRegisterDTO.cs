using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
    public record UserRegisterDTO
    {
        [Required(ErrorMessage = "DisplayName is Required")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "PhoneNumber is Required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }
        [Required]
        public string  Role { get; set; }
    }
}
