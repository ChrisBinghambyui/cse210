using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        int squaredNumber = SquareNumber(userNumber);

        int byear;
        PromptUserBirthYear(out byear);

        DisplayResult(userName, squaredNumber, byear);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.WriteLine("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }
    
    static int PromptUserNumber()
    {
        Console.WriteLine("Please enter your favorite number: ");
        string num = Console.ReadLine();
        int favnumber = int.Parse(num);
        return favnumber;
    }

    static void PromptUserBirthYear(out int byear)
    {
        Console.WriteLine("Please enter the year you were born: ");
        string yr = Console.ReadLine();
        byear = int.Parse(yr);
    }

    static int SquareNumber(int number)
    {
        int squared = number * number;
        return squared;
    }

    static void DisplayResult(string name, int squaredNumber, int byear)
    {
        int cyear = 2026;
        int age = cyear - byear;
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
        Console.WriteLine($"{name}, you will turn {age} this year.");
    }
}