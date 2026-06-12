using System;

class Program
{
    private int _menuChoice;

    public Program()
    {
        _menuChoice = 0;
    }

    public int Menu()
    {
        Console.WriteLine();
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Start Breathing Activity");
        Console.WriteLine("  2. Start Reflection Activity");
        Console.WriteLine("  3. Start Listing Activity");
        Console.WriteLine("  4. Quit");
        Console.Write("Select a choice from the menu: ");

        int choice = int.Parse(Console.ReadLine());
        return choice;
    }

    static void Main(string[] args)
    {
        Program program = new Program();

        program._menuChoice = 0;

        while (program._menuChoice != 4)
        {
            program._menuChoice = program.Menu();

            switch (program._menuChoice)
            {
                case 1:
                    Breathing breathing = new Breathing();
                    breathing.RunActivity();
                    break;

                case 2:
                    Reflection reflection = new Reflection();
                    reflection.RunActivity();
                    break;

                case 3:
                    Listing listing = new Listing();
                    listing.RunActivity();
                    break;

                case 4:
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Not a valid option, please try again.");
                    break;
            }
        }
    }
}