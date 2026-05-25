using ProjectTaskManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto dto);

        Task<List<TaskDto>> GetByProjectAsync(int projectId);

        Task<TaskDto> UpdateStatusAsync(
            int taskId,
            UpdateTaskStatusDto dto);

        Task<bool> DeleteAsync(int taskId);
    }
}
