using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

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
        Console.Write("\t>");

        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("m/d/yyyy");

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
        Console.Write("Name for the journal file? ");
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
        Console.Write("Name of the journal file? ");
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

        Console.WriteLine($"Loaded {_entries.Count} from {filename}.");
    }

    public void ExportJournalToExcel()
    {
        // using (StreamWriter outputFile = new StreamWriter(filename))
        // {
        //     outputFile.WriteLine("Date,Prompt,Response");
        //     foreach (Entry entry in _entries)
        //     {
        //         outputFile.WriteLine(entry.ToCsvFormat());
        //     }
        // }
        //
        // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        // using (var package = new ExcelPackage())
        // {
        //     var ws = package.Workbook.Worksheets.Add("Journal");
        //     // populate cells
        //     // package.SaveAs(new FileInfo(filename));
        // }
        Console.Write("Name for the export file (don't forfet .xlsx): ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            filename = "journal.csv";
        }

        string ext = Path.GetExtension(filename);
        if (string.IsNullOrEmpty(ext))
        {
            filename = filename + ".csv";
            ext = ".csv";
        }

        if (ext.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Journal");
                ws.Cell(1, 1).Value = "Date";
                ws.Cell(1, 2).Value = "Prompt";
                ws.Cell(1, 3).Value = "Response";

                int row = 2;
                foreach (Entry entry in _entries)
                {
                    ws.Cell(row, 1).Value = entry.Date;
                    ws.Cell(row, 2).Value = entry.Prompt;
                    ws.Cell(row, 3).Value = entry.Response;
                    row++;
                }

                wb.SaveAs(filename);
            }

            Console.WriteLine($"Journal exported to {filename}.");
            return;
        }

        if (!ext.Equals(".csv", StringComparison.OrdinalIgnoreCase))
        {
            filename = filename + ".csv";
        }

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine("Date,Prompt,Response");

            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine(entry.ToCsvFormat());
            }
        }

        Console.WriteLine($"Journal exported to {filename}.");
    }
}
