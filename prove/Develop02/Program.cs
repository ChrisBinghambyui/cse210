using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Journal!");
        Console.WriteLine();

        // Creativity feature: export the journal to an Excel-friendly CSV file.

        Journal journal = new Journal();
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Write New Entry");
            Console.WriteLine("2. Display Journal");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Excel");
            Console.WriteLine("6. Quit");
            Console.Write("What would you like to do? (1-6) ");

            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    journal.SaveJournal();
                    break;
                case "4":
                    journal.LoadJournal();
                    break;
                case "5":
                    journal.ExportJournalToExcel();
                    break;
                case "6":
                    keepRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a number from 1 to 6.");
                    break;
            }

            Console.WriteLine();
        }
    }
}