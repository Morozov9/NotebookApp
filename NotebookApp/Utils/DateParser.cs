namespace NotebookApp.Utils;
using System.Text.RegularExpressions;
using System.Globalization;

public class DateParser
{
    private readonly Regex _regexAllDate = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\.(0[1-9]|1[0-2])\.\d{4}(?=\s|$)");
    private readonly Regex _regexAllDateShortYear = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\.(0[1-9]|1[0-2])\.\d{2}(?=\s|$)");
    private readonly Regex _regexWithoutYear = new(@"(?<!\d)((0[1-9]|[12]\d|3[01]|[1-9])(?i)\.(0[1-9]|1[0-2]))(?=\s|$)");
    private readonly Regex _regexWithShortWords = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(янв|фев|мар|апр|ма[йя]|июн|июл|авг|сен|окт|нояб|дек)(?=\s|\.|$)");
    private readonly Regex _regexWithWords = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(январ|феврал|март|апрел|ма[йя]|июн|июл|август|сентябр|октябр|ноябр|декабр)[а-яА-ЯёЁ]*\b");
    
    

    private readonly Dictionary<string, string> _monthNumbers = new()
        {
            { "янв", "01" },
            { "фев", "02" },
            { "мар", "03" },
            { "апр", "04" },
            { "май", "05" },
            { "июн", "06" },
            { "июл", "07" },
            { "авг", "08" },
            { "сен", "09" },
            { "окт", "10" },
            { "нояб", "11" },
            { "дек", "12" }
        };


    public List<DateTime> ParseDates(string text)
    {
        List<DateTime> dates = new();
        
        var matches1 = _regexAllDate.Matches(text);
        var matches2 = _regexWithoutYear.Matches(text);
        var matches3 = _regexWithShortWords.Matches(text);
        var matches4 = _regexWithWords.Matches(text);
        var matches5 = _regexAllDateShortYear.Matches(text);
        
        
        foreach (Match match in matches1)
        {
            if (DateTime.TryParse(match.Value, out var date) && date > DateTime.Today)
                dates.Add(date);
        }


        foreach (Match match in matches2)
        {
            if (DateTime.TryParse(match.Value + "." + DateTime.Today.Year, out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
        }
        
                
        foreach (Match match in matches5)
        {
            var numMonth = match.Value.Split(".");

            if (DateTime.TryParse($"{numMonth[0]}.{numMonth[1]}.{DateTime.Today.Year.ToString()[..2]}{numMonth[2]}", out var date) && date > DateTime.Today)
                
                dates.Add(date);
        }
        
        

        foreach (Match match in matches3)
        {
            var numMonth = match.Value.Split(' ');

            if (DateTime.TryParse(numMonth[0] + '.' + _monthNumbers[numMonth[1]] + '.' + DateTime.Today.Year,
                    out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
            
        }

        foreach (Match match in matches4)
        {
            var numMonth = match.Value.Split(' ');

            if(DateTime.TryParse(numMonth[0] + '.' + _monthNumbers[numMonth[1][..3]] + '.' + DateTime.Today.Year, out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
        }


        return dates;
    }



}