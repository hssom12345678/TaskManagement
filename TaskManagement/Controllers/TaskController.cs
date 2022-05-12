using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TaskManagement.Dtos;
using TaskManagement.Extentions;
using TaskManagement.Models;
using TaskManagement.Repository.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        #region Private Properties
        private readonly ITaskRepository _taskRepo;
        #endregion

        #region Constructor
        public TaskController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }
        #endregion

        // POST api/<TaskController>
        [HttpPost]
        public async Task<ActionResult<TaskDTO>> Post(TaskCreateDTO taskDto)
        {
            var highPriorityTaskCount = await _taskRepo.GetHightPriorityTaskCount();
            if (highPriorityTaskCount >= 100)
                return Ok("Task limit reached please try again after some time");
            if (taskDto.DueDate < DateTime.Now.Date)
                return Ok("Due date can not be a past date");
            Tasks task = new()
            {
                Id = Guid.NewGuid(),
                Name = taskDto.Name,
                Description = taskDto.Description,
                StartDate = taskDto.StartDate,
                DueDate = taskDto.DueDate,
                EndDate = taskDto.EndDate,
                Status = taskDto.Status,
                Priority = taskDto.Priority,
            };

            await _taskRepo.CreateTaskAsync(task);

            //return Ok("Task created successfully");
            return CreatedAtAction(nameof(Tasks), new { id = task.Id }, task.AsDto());
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, TaskUpdateDTO dto)
        {
            var highPriorityTaskCount = await _taskRepo.GetHightPriorityTaskCount();
            if (highPriorityTaskCount >= 100)
                return Ok("Task limit reached please try again after some time");
            if (dto.DueDate < DateTime.Now)
                return Ok("Due date can not be a past date");

            var existingItem = await _taskRepo.GetTaskAsync(id);

            if (existingItem is null)
            {
                return Ok("Task not found");
            }

            existingItem.Name = dto.Name;
            existingItem.Description = dto.Description;
            existingItem.StartDate = dto.StartDate;
            existingItem.DueDate = dto.DueDate;
            existingItem.EndDate = dto.EndDate;
            existingItem.Status = dto.Status;
            existingItem.Priority = dto.Priority;

            await _taskRepo.UpdateTaskAsync(existingItem);

            //return Ok("Task updated successfully");
            return NoContent();
        }

    }
}
