using TaskManagement.Dtos;
using TaskManagement.Models;

namespace TaskManagement.Extentions
{
    public static class Extentions
    {
        public static TaskDTO AsDto(this Tasks task)
        {
            return new TaskDTO(task.Id, task.Name, task.Description, task.StartDate,task.DueDate, task.EndDate, task.Status, task.Priority);
        }
    }
}
