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
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public ProjectService(ApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        } 
        
        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = _currentUser.UserId
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<List<ProjectDto>> GetAllAsync()
        {
            return await _context.Projects
                .Where(p => p.UserId == _currentUser.UserId)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }
        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Where(p => p.Id == id && p.UserId == _currentUser.UserId)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt
                })
                .FirstOrDefaultAsync();
        }
        public async Task<ProjectDto> UpdateAsync(
        int id,
        UpdateProjectDto dto)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == _currentUser.UserId);

            if (project == null)
                throw new Exception("Project not found");

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == _currentUser.UserId);

            if (project == null)
                return false;

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return true;

        }
    }
}