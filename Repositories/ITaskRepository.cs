using TaskApi.Models;

namespace TaskApi.Repositories;

public interface ITaskRepository
{
    Task<(IEnumerable<TodoTask> Items, int TotalCount)> GetTasksAsync(
        TodoStatus? status,
        DateTime? dueFrom,
        DateTime? dueTo,
        int pageNumber,
        int pageSize);

    Task<TodoTask?> GetByIdAsync(int id);
    Task<TodoTask> AddAsync(TodoTask task);
    Task UpdateAsync(TodoTask task);
    Task DeleteAsync(TodoTask task);
}
