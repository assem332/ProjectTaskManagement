using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Domain.Entities
{
    public  class ProjectTask
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "ToDo";

        public DateTime DueDate { get; set; }

        public string Priority { get; set; } = "Medium";

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
