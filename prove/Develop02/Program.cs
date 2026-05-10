using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Journal Program!");
        Console.WriteLine();

        Journal journal = new Journal();
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Write a New Entry");
            Console.WriteLine("2. Display the Journal");
            Console.WriteLine("3. Save the Journal");
            Console.WriteLine("4. Load the Journal");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? (1-5) ");

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
                    keepRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a number from 1 to 5.");
                    break;
            }

            Console.WriteLine();
        }
    }
}