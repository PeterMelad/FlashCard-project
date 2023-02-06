using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Register
    {
        [Required]
        public string Email { get; set; }
        public Login info { get; set; }
    }
}
