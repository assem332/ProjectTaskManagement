using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTaskManagement.Application.DTOs;

namespace ProjectTaskManagement.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);

    Task<List<ProjectDto>> GetAllAsync();

    Task<ProjectDto?> GetByIdAsync(int id);

    Task<ProjectDto> UpdateAsync(
        int id,
        UpdateProjectDto dto);

    Task<bool> DeleteAsync(int id);
}


