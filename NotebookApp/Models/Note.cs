namespace NotebookApp.Models;

public class Note
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tag { get; set; } = "Без тега"; // Новое поле для тега
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ReminderTime { get; set; }
    public bool IsPinned { get; set; } = false;
    public DateTime EditedAt { get; set; } = DateTime.Now;
}