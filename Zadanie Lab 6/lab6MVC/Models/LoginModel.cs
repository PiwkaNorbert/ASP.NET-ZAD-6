using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab6MVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "User password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember you?")]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}
