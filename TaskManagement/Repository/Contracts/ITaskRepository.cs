using System.Linq.Expressions;
using TaskManagement.Models;

namespace TaskManagement.Repository.Contracts
{
    public interface ITaskRepository
    {
        Task<Tasks> GetTaskAsync(Guid id);
        Task<IEnumerable<Tasks>> GetTasksAsync();
        Task<IEnumerable<Tasks>> GetWhereAsync(Expression<Func<Tasks, bool>> predicate);
        Task<int> GetHightPriorityTaskCount();
        Task CreateTaskAsync(Tasks task);
        Task<bool> UpdateTaskAsync(Tasks task);
        Task DeleteTaskAsync(Guid id);
    }
}
