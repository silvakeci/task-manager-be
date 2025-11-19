using TaskApi.Dtos;
using TaskApi.Models;
using TaskApi.Repositories;

namespace TaskApi.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<TaskDto>> GetTasksAsync(
        TodoStatus? status,
        DateTime? dueFrom,
        DateTime? dueTo,
        int pageNumber,
        int pageSize)
    {
        var (items, totalCount) = await _repo.GetTasksAsync(status, dueFrom, dueTo, pageNumber, pageSize);

        var dtos = items.Select(MapToDto);

        return new PagedResult<TaskDto>
        {
            Items = dtos,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var task = await _repo.GetByIdAsync(id);
        return task is null ? null : MapToDto(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskRequest request)
    {
        var task = new TodoTask
        {
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            DueDate = request.DueDate
        };

        var created = await _repo.AddAsync(task);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskRequest request)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return false;

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.Status = request.Status;
        existing.DueDate = request.DueDate;

        await _repo.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return false;

        await _repo.DeleteAsync(existing);
        return true;
    }

    private static TaskDto MapToDto(TodoTask task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        Status = task.Status,
        DueDate = task.DueDate
    };
}
