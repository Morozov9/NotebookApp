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
        
        MatchCollection matches1 = _regexAllDate.Matches(text);
        MatchCollection matches2 = _regexWithoutYear.Matches(text);
        MatchCollection matches3 = _regexWithShortWords.Matches(text);
        MatchCollection matches4 = _regexWithWords.Matches(text);
        MatchCollection matches5 = _regexAllDateShortYear.Matches(text);
        
        
        foreach (Match match in matches1)
        {
            dates.Add(DateTime.Parse(match.Value));
        }


        foreach (Match match in matches2)
        {
            dates.Add(DateTime.Parse(match.Value + "." + DateTime.Today.Year));
        }
        
                
        foreach (Match match in matches5)
        {
            var a = match.Value.Split(".");
            
            dates.Add(DateTime.Parse($"{a[0]}.{a[1]}.{DateTime.Today.Year.ToString()[..2]}{a[2]}"));
        }
        
        

        foreach (Match match in matches3)
        {
            var sDate = match.Value.Split(' ');
        
            var date = DateTime.Parse(sDate[0] + '.' + _monthNumbers[sDate[1]] + '.' + DateTime.Today.Year);
            if (DateTime.Now > date)
            {
                dates.Add(DateTime.Parse(sDate[0] + '.' + _monthNumbers[sDate[1]] + '.' + DateTime.Today.AddYears(1).Year));
            }
            else
            {
                dates.Add(date);
            }
        }

        foreach (Match match in matches4)
        {
            var sDate = match.Value.Split(' ');

            var date = DateTime.Parse(sDate[0] + '.' + _monthNumbers[sDate[1][..3]] + '.' + DateTime.Today.Year);
            if (DateTime.Now > date)
            {
                dates.Add(DateTime.Parse(sDate[0] + '.' + _monthNumbers[sDate[1][..3]] + '.' +
                                         DateTime.Today.AddYears(1).Year));
            }
            else
            {
                dates.Add(date);
            }
        }


        return dates;
    }



}