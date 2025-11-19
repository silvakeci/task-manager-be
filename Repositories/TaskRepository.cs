using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TasksDbContext _context;

    public TaskRepository(TasksDbContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<TodoTask> Items, int TotalCount)> GetTasksAsync(
        TodoStatus? status,
        DateTime? dueFrom,
        DateTime? dueTo,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Tasks.AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        if (dueFrom.HasValue)
        {
            query = query.Where(t => t.DueDate >= dueFrom.Value);
        }

        if (dueTo.HasValue)
        {
            query = query.Where(t => t.DueDate <= dueTo.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.DueDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TodoTask> AddAsync(TodoTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task UpdateAsync(TodoTask task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoTask task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}
