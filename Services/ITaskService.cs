using TaskApi.Dtos;
using TaskApi.Models;

namespace TaskApi.Services;

public interface ITaskService
{
    Task<PagedResult<TaskDto>> GetTasksAsync(
        TodoStatus? status,
        DateTime? dueFrom,
        DateTime? dueTo,
        int pageNumber,
        int pageSize);

    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskRequest request);
    Task<bool> UpdateAsync(int id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(int id);
}
