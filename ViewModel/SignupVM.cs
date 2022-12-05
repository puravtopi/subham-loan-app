using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ViewModel
{
    public class SignupVM
    {

        public string TenantName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Initials { get; set; }
        public string Phone { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

        public bool IsTrial { get; set; }
    }
}
