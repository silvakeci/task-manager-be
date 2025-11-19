using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models;

public class TodoTask
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public TodoStatus Status { get; set; } = TodoStatus.ToDo;

    [Required]
    public DateTime DueDate { get; set; }
}
