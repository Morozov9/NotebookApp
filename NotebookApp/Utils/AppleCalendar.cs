using System.Text;

namespace NotebookApp.Utils;

using Models;

public static class AppleCalendar
{


    public static string AppleCalendarFile(Note note)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        StringBuilder builder = new();

        builder.Append(
            "BEGIN:VCALENDAR\r\n" +
            "VERSION:2.0\r\n" +
            "PRODID:-//IIT//PrI-101//NotebookApp\r\n"
        );
        if (note.Type == NoteType.Text)
        {
            
            foreach (var date in note.Dates)
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
            
        }
        else
        {
            foreach (var dateEvent in note.DateEvents)
            {
                string formattedDate = dateEvent.Date.ToString("yyyyMMdd");
                string formattedDateEnd = dateEvent.Date.AddDays(1).ToString("yyyyMMdd");

                var uid = string.Create(15, chars,
                    (span, alphabet) => { Random.Shared.GetItems(alphabet.AsSpan(), span); });

                builder.Append(
                    "BEGIN:VEVENT\r\n" +
                    $"UID:{uid}@IIT.NotebookApp.ru\r\n" +
                    $"DTSTART;VALUE=DATE:{formattedDate}\r\n" +
                    $"DTEND;VALUE=DATE:{formattedDateEnd}\r\n" +
                    $"SUMMARY:{note.Title}\r\n" +
                    $"DESCRIPTION:{dateEvent.Event}\r\n" +
                    "END:VEVENT\r\n"
                );
            }
        }



        builder.Append("END:VCALENDAR");

        return builder.ToString();
    }
    
    
    
    public static string AppleCalendarFileNew(Note note, List<DateEvent> dateEvents)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        StringBuilder builder = new();

        builder.Append(
            "BEGIN:VCALENDAR\r\n" +
            "VERSION:2.0\r\n" +
            "PRODID:-//IIT//PrI-101//NotebookApp\r\n"
        );

        foreach (var dateEvent in dateEvents)
        {
            string formattedDate = dateEvent.Date.ToString("yyyyMMdd");
            string formattedDateEnd = dateEvent.Date.AddDays(1).ToString("yyyyMMdd");

            var uid = string.Create(15, chars,
                (span, alphabet) => { Random.Shared.GetItems(alphabet.AsSpan(), span); });

            builder.Append(
                "BEGIN:VEVENT\r\n" +
                $"UID:{uid}@IIT.NotebookApp.ru\r\n" +
                $"DTSTART;VALUE=DATE:{formattedDate}\r\n" +
                $"DTEND;VALUE=DATE:{formattedDateEnd}\r\n" +
                $"SUMMARY:{note.Title}\r\n" +
                $"DESCRIPTION:{dateEvent.Event}\r\n" +
                "END:VEVENT\r\n"
            );
        }



        builder.Append("END:VCALENDAR");

        return builder.ToString();
    }
    
        
        
    public static string AppleCalendarFileNew(Note note, List<DateTime> dates)
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