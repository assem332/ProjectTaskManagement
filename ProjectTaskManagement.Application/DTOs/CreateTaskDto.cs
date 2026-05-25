using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Application.DTOs
{
    public class CreateTaskDto
    {
        [Required]

        public string Title { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public string Priority { get; set; } = "Medium";

        public int ProjectId { get; set; }
    }
}
