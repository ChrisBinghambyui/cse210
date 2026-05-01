using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep3 World!");
        Console.WriteLine("Choose a number: ");
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, 11);
        int guess = 0;
        int counter = 0;
        while (guess != number)
        {
            Console.Write("Enter a guess: ");
            string gup = Console.ReadLine();
            guess = int.Parse(gup);
            counter++;
            if (number >  guess)
            {
                Console.Write("Higher... ");
            }
            else if (number < guess)
            {
                Console.Write("Lower... ");
            }
            else
            {
                Console.Write("You got it! ");
            }
        }
        Console.WriteLine($"It took you {counter} attempts.");
        
        
    }
};
