using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries;
    private PromptGenerator _promptGenerator;

    public Journal()
    {
        _entries = new List<Entry>();
        _promptGenerator = new PromptGenerator();
    }

    public void WriteNewEntry()
    {
        string prompt = _promptGenerator.GetRandomPrompt();
        Console.WriteLine(prompt);
        Console.Write("Your response: ");

        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("M/d/yyyy");

        Entry entry = new Entry(prompt, response, date);
        _entries.Add(entry);
    }

    public void DisplayJournal()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
            Console.WriteLine();
        }
    }

    public void SaveJournal()
    {
        Console.Write("What is the filename for the journal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ToFileFormat());
            }
        }

        Console.WriteLine($"Journal saved to {filename}.");
    }

    public void LoadJournal()
    {
        Console.Write("What is the filename for the journal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' was not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            string[] parts = line.Split(new char[] { '|' }, 3);

            if (parts.Length == 3)
            {
                Entry entry = new Entry(parts[1], parts[2], parts[0]);
                _entries.Add(entry);
            }
        }

        Console.WriteLine($"Loaded {_entries.Count} entries from {filename}.");
    }
}
