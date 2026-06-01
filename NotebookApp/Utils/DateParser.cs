namespace NotebookApp.Utils;
using System.Text.RegularExpressions;

public class DateParser
{
    private readonly Regex _regexAllDate = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\.(0[1-9]|1[0-2])\.\d{4}(?=\s|$)");
    private readonly Regex _regexAllDateShortYear = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\.(0[1-9]|1[0-2])\.\d{2}(?=\s|$)");
    private readonly Regex _regexNumMonth = new(@"(?<!\d)((0[1-9]|[12]\d|3[01]|[1-9])(?i)\.(0[1-9]|1[0-2]))(?=\s|$)");
    private readonly Regex _regexShortWords = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(янв|фев|мар|апр|ма[йя]|июн|июл|авг|сен|окт|нояб|дек)(?!\s+\d{4})(?=\s|\.|$)");
    private readonly Regex _regexWords = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(январ|феврал|март|апрел|ма[йя]|июн|июл|август|сентябр|октябр|ноябр|декабр)[а-яА-ЯёЁ]*\b(?!\s+\d{4})(?=\s|\.|$)");
    
    
    private readonly Regex _regexShortWordsYear = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(янв|фев|мар|апр|ма[йя]|июн|июл|авг|сен|окт|нояб|дек)\s+(?i)\d{4}(?=\s|\.|$)");
    private readonly Regex _regexWordsYear = new(@"(?<!\d)(0[1-9]|[12]\d|3[01]|[1-9])\s+(?i)(январ|феврал|март|апрел|ма[йя]|июн|июл|август|сентябр|октябр|ноябр|декабр)[а-яА-ЯёЁ]*\b\s+(?i)\d{4}");
    
    private readonly Regex _regexAllDateEng = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\/(0[1-9]|1[0-2])\/\d{4}(?=\s|$)");
    private readonly Regex _regexAllDateShortYearEng = new(@"(?<!\d)((0[1-9]|[12]\d|3[01])|[1-9])(?i)\.(0[1-9]|1[0-2])\/\d{2}(?=\s|$)");
    private readonly Regex _regexNumMonthEng = new(@"(?<!\d)((0[1-9]|[12]\d|3[01]|[1-9])(?i)\/(0[1-9]|1[0-2]))(?=\s|$)");
    
    
    

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
        
        var mRegexAllDate = _regexAllDate.Matches(text);
        var mRegexNumMonth = _regexNumMonth.Matches(text);
        var mRegexAllDateShortYear = _regexAllDateShortYear.Matches(text);
        
        var mRegexShortWords = _regexShortWords.Matches(text);
        var mRegexWords = _regexWords.Matches(text);
        
        var mRegexAllDateEng = _regexAllDateEng.Matches(text);
        var mRegexNumMonthEng = _regexNumMonthEng.Matches(text);
        var mRegexAllDateShortYearEng = _regexAllDateShortYearEng.Matches(text);
        
        var mRegexShortWordsYear = _regexShortWordsYear.Matches(text);
        var mRegexWordsYear = _regexWordsYear.Matches(text);


        
        foreach (Match match in mRegexAllDate.Concat(mRegexAllDateEng))
        {
            if (DateTime.TryParse(match.Value, out var date) && date > DateTime.Today)
                dates.Add(date);
        }


        foreach (Match match in mRegexNumMonth.Concat(mRegexNumMonthEng))
        {
            if (DateTime.TryParse(match.Value + "." + DateTime.Today.Year, out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
        }
        
                
        foreach (Match match in mRegexAllDateShortYear.Concat(mRegexAllDateShortYearEng))
        {
            var numMonth = match.Value.Split(".");

            if (DateTime.TryParse($"{numMonth[0]}.{numMonth[1]}.{DateTime.Today.Year.ToString()[..2]}{numMonth[2]}", out var date) && date > DateTime.Today)
                
                dates.Add(date);
        }
        
        

        foreach (Match match in mRegexShortWords)
        {
            var numMonth = match.Value.Split(' ');

            if (DateTime.TryParse(numMonth[0] + '.' + _monthNumbers[numMonth[1]] + '.' + DateTime.Today.Year,
                    out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
            
        }

        foreach (Match match in mRegexWords)
        {
            var numMonth = match.Value.Split(' ');

            if(DateTime.TryParse(numMonth[0] + '.' + _monthNumbers[numMonth[1][..3]] + '.' + DateTime.Today.Year, out var date))
                dates.Add(DateTime.Now > date ? date.AddYears(1) : date);
        }
        
        

        foreach (Match match in mRegexShortWordsYear)
        {
            var numMonthYear = match.Value.Split(' ');

            if (DateTime.TryParse(numMonthYear[0] + '.' + _monthNumbers[numMonthYear[1]] + '.' + numMonthYear[2],
                    out var date) && date > DateTime.Today)
                dates.Add(date);
            
        }

        foreach (Match match in mRegexWordsYear)
        {
            var numMonthYear = match.Value.Split(' ');

            if(DateTime.TryParse(numMonthYear[0] + '.' + _monthNumbers[numMonthYear[1][..3]] + '.' + DateTime.Today.Year, out var date) && date > DateTime.Today)
                dates.Add(date);
        }
        
        


        return dates;
    }



}