using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.DTOs;
using ProjectTaskManagement.Application.Interfaces;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectTaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        public TaskService(ApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == dto.ProjectId && p.UserId == _currentUser.UserId);
            if (project == null)
                throw new Exception("Project not found");
            var task = new ProjectTask
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                Status = "ToDo",
                ProjectId = dto.ProjectId
            };
            await _context.Tasks.AddAsync(task);

            await _context.SaveChangesAsync();
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId
            };

        }

        public async Task<List<TaskDto>> GetByProjectAsync(int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId).Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId
            })
                .ToListAsync();
        }
        public async Task<TaskDto> UpdateStatusAsync(int taskId, UpdateTaskStatusDto dto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
                throw new Exception("Task not found");
            task.Status = dto.Status;
            await _context.SaveChangesAsync();
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId
            };
        }
        public async Task<bool> DeleteAsync(int taskId)
        {
            var task = await _context.Tasks
           .FirstOrDefaultAsync(
               x => x.Id == taskId);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();

            return true;
        }
    }

 }
