namespace NotebookApp.Utils;

using Models;

public static class GoogleCalendar
{
    public static string LinkToGoogleCalendar(Note note, DateTime date)
    {
        string title = note.Title;
        string description = note.Content;

        string formattedDate = date.ToString("yyyyMMdd");
        string formattedDateEnd = date.AddDays(1).ToString("yyyyMMdd");

        var encodedTitle = Uri.EscapeDataString(title);
        var encodedDescription = Uri.EscapeDataString(description);

        string googleCalendarUrl = $"https://calendar.google.com/calendar/u/0/r/eventedit" +
                                   $"?text={encodedTitle}" +
                                   $"&dates={formattedDate}/{formattedDateEnd}" +
                                   $"&details={encodedDescription}";

        return googleCalendarUrl;
    }
}