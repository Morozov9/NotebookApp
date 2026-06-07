namespace NotebookApp.Models;

public class Note
{
    
    public NoteType Type { get; set; } =  NoteType.Text;
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<OneTask> Tasks { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Tag { get; set; } = "Без тега";
    public string Color { get; set; } = "#ffffff";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsPinned { get; set; }
    public DateTime EditedAt { get; set; } = DateTime.Now;
    public List<DateTime> Dates { get; set; } = new();
    public bool Star { get; set; }
    public bool IsAddedToCalendar { get; set; }
    public List<DateEvent> DateEvents { get; set; } = new();
    // public List<DateTime> newDates { get; set; } = new();
    
}


public class OneTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

public enum NoteType
{
    Text,
    Tasks
}

public record DateEvent(DateTime Date, string Event);
