using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep1 World!");
        Console.WriteLine("What is your first name? ");
        string fname = Console.ReadLine();
        Console.WriteLine("What is your last name? ");
        string lname = Console.ReadLine();

        Console.WriteLine($"Your name is {lname}, {fname} {lname}.");

    }
}