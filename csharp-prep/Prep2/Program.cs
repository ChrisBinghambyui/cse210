using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep2 World!");
        Console.WriteLine("Enter a grade: ");
        string num = Console.ReadLine();
        int number = int.Parse(num);
        string grade = "";
        if (number >= 90)
        {
            grade = "A";
        }
        else if (number >= 80)
        {
            grade = "B";
        }
        else if (number >= 70)
        {
            grade = "C";
        }
        else if (number >= 60)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }
        if (number >= 70)
        {
            Console.WriteLine("Congratulations on passing the class.");
        }
        else
        {
            Console.WriteLine("Sorry, but you failed the class.");
        }
        
        Console.WriteLine($"Your grade is {grade}");
        
    }
}