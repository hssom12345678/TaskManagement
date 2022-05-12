using System.ComponentModel.DataAnnotations;
using TaskManagement.Models;

namespace TaskManagement.Dtos
{

    public record TaskDTO(Guid Id, string Name, string Description, DateTime StartDate, DateTime DueDate, DateTime EndDate, Status Status, Priority Priority);
    public record TaskCreateDTO([Required] string Name, string Description, DateTime StartDate, DateTime DueDate, DateTime EndDate, [EnumDataType(typeof(Status), ErrorMessage = "Status type value doesn't exist within enum")] Status Status, [EnumDataType(typeof(Priority), ErrorMessage = "Priority type value doesn't exist within enum")] Priority Priority);
    public record TaskUpdateDTO([Required] string Name, string Description, DateTime StartDate, DateTime DueDate, DateTime EndDate, [EnumDataType(typeof(Status), ErrorMessage = "Status type value doesn't exist within enum")] Status Status, [EnumDataType(typeof(Priority), ErrorMessage = "Priority type value doesn't exist within enum")] Priority Priority);
}
