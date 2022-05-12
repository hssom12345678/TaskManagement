using System.Linq.Expressions;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Repository.Contracts;

namespace TaskManagement.Repository
{
    public class TaskRepository : ITaskRepository
    {
        #region Private-Properties
        private readonly TaskDbContext _dbContext;
        #endregion
        #region Constructor
        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        public async Task CreateTaskAsync(Tasks task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public TaskDbContext Get_dbContext()
        {
            return _dbContext;
        }

        public async Task<Tasks> GetTaskAsync(Guid id)
        {
            var result = _dbContext.Tasks.Where(x => x.Id == id).FirstOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Tasks>> GetTasksAsync()
        {
            var result = _dbContext.Tasks.ToList();
            return await Task.FromResult(result);
        }
        public async Task<IEnumerable<Tasks>> GetWhereAsync(Expression<Func<Tasks, bool>> predicate) => _dbContext.Set<Tasks>().Where(predicate).ToList();
        public async Task<int> GetHightPriorityTaskCount()
        {
            var hightPriority = _dbContext.Tasks.Where(x => x.Priority == Priority.High && x.Status != Status.Finished).ToList();
            if (hightPriority.Count == 0) return 0;
            var query = hightPriority.GroupBy(i => i.DueDate.Date)
            .Select(group => new
            {
                Metric = group.Key,
                Count = group.Count()
            })
             .OrderBy(x => x.Metric);
            var coun = query.ToList();
            return query.Max(x => x.Count); ;
        }
        public async Task<bool> UpdateTaskAsync(Tasks task)
        {
            var result = _dbContext.Tasks.Where(x => x.Id == task.Id).FirstOrDefault();
            if (result != null)
            {
                _dbContext.Tasks.Update(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
