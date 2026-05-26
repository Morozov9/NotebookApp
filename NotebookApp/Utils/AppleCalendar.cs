using System.Text;

namespace NotebookApp.Utils;

using Models;

public static class AppleCalendar
{
    public static string AppleCalendarFile(Note note, DateTime date)
    {
        string formattedDate = date.ToString("yyyyMMdd");
        string formattedDateEnd = date.AddDays(1).ToString("yyyyMMdd");

        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var uid = string.Create(15, chars, (span, alphabet) => { Random.Shared.GetItems(alphabet.AsSpan(), span); });


        string appleFile = $"BEGIN:VCALENDAR\r\n" +
                           $"VERSION:2.0\r\n" +
                           $"PRODID:-//IIT//PrI-101//NotebookApp\r\n" +
                           $"BEGIN:VEVENT\r\n" +
                           $"UID:{uid}@IIT.NotebookApp.ru\r\n" +
                           $"DTSTART;VALUE=DATE:{formattedDate}\r\n" +
                           $"DTEND;VALUE=DATE:{formattedDateEnd}\r\n" +
                           $"SUMMARY:{note.Title}\r\n" +
                           $"DESCRIPTION:{note.Content}\r\n" +
                           $"END:VEVENT\r\n" +
                           $"END:VCALENDAR";

        return appleFile;
    }

    public static string AppleCalendarFile(Note note, List<DateTime> dates)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        StringBuilder builder = new();

        builder.Append(
            "BEGIN:VCALENDAR\r\n" +
            "VERSION:2.0\r\n" +
            "PRODID:-//IIT//PrI-101//NotebookApp\r\n"
        );

        foreach (var date in dates)
        {
            string formattedDate = date.ToString("yyyyMMdd");
            string formattedDateEnd = date.AddDays(1).ToString("yyyyMMdd");

            var uid = string.Create(15, chars,
                (span, alphabet) => { Random.Shared.GetItems(alphabet.AsSpan(), span); });

            builder.Append(
                "BEGIN:VEVENT\r\n" +
                $"UID:{uid}@IIT.NotebookApp.ru\r\n" +
                $"DTSTART;VALUE=DATE:{formattedDate}\r\n" +
                $"DTEND;VALUE=DATE:{formattedDateEnd}\r\n" +
                $"SUMMARY:{note.Title}\r\n" +
                $"DESCRIPTION:{note.Content}\r\n" +
                "END:VEVENT\r\n"
            );
        }

        builder.Append("END:VCALENDAR");

        return builder.ToString();
    }
}