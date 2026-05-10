using System;

public class Entry
{
    private string _prompt;
    private string _response;
    private string _date;

    public Entry()
    {
        _prompt = "";
        _response = "";
        _date = "";
    }

    public Entry(string prompt, string response, string date)
    {
        _prompt = prompt;
        _response = response;
        _date = date;
    }

    public void Display()
    {
        Console.WriteLine($"{_date} - {_prompt}");
        Console.WriteLine(_response);
    }

    public string ToFileFormat()
    {
        return $"{_date}|{_prompt}|{_response}";
    }
}
