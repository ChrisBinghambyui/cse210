using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Prep4 World!");
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        List<int> numbers = new List<int>();

        int entry = 1;
        int counter = 0;
        while (entry != 0)
        {
            Console.WriteLine("Enter number: ");
            string raw = Console.ReadLine();
            entry = int.Parse(raw);
            if (entry != 0)
            {
                numbers.Add(entry);
                counter++;
            }
        }

        int sum = 0;
        double avg = 0;
        int max = 0;

        if (numbers.Count > 0)
        {
            max = numbers[0];

            foreach (int number in numbers)
            {
                sum += number;
                if (number > max)
                {
                    max = number;
                }
            }

            avg = (double)sum / numbers.Count;
        }

        Console.WriteLine($"The sum is {sum}");
        Console.WriteLine($"The average is {avg}");
        Console.WriteLine($"The highest number is {max}");
    }
}