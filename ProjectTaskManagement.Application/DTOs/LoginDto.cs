using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Application.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]

        public string? Email { get; set; } 
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string? Password { get; set; } 
    }
}
