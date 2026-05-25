using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.DTOs;
using ProjectTaskManagement.Application.Interfaces;

namespace ProjectTaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<IActionResult> Create( CreateTaskDto dto)
        {
            var result =
                await _taskService.CreateAsync(dto);

            return Ok(result);
        }
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProject(int projectId)
        {
            var result =
                await _taskService
                .GetByProjectAsync(projectId);

            return Ok(result);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult>UpdateStatus(int taskId,UpdateTaskStatusDto dto)
        {
            var result =
                await _taskService
                .UpdateStatusAsync(
                    taskId,
                    dto);

            return Ok(result);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            var result =
                await _taskService
                .DeleteAsync(taskId);

            if (!result)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }

}
