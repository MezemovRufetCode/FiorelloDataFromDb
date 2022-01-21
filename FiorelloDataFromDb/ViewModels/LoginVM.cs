using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloDataFromDb.ViewModels
{
    public class LoginVM
    {
        [Required]
        [StringLength(maximumLength:70)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool KeepSigned { get; set; }
    }
}
