using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction first = new Fraction();
        Console.WriteLine(first.GetFractionString());
        Console.WriteLine(first.GetDecimalValue());

        Fraction second = new Fraction(5);
        Console.WriteLine(second.GetFractionString());
        Console.WriteLine(second.GetDecimalValue());

        Fraction third = new Fraction(3, 4);
        Console.WriteLine(third.GetFractionString());
        Console.WriteLine(third.GetDecimalValue());

        Fraction fourth = new Fraction(1, 3);
        Console.WriteLine(fourth.GetFractionString());
        Console.WriteLine(fourth.GetDecimalValue());

        Fraction checkSetGet = new Fraction();
        checkSetGet.SetTop(6);
        checkSetGet.SetBottom(7);
        Console.WriteLine(checkSetGet.GetTop());
        Console.WriteLine(checkSetGet.GetBottom());
        Console.WriteLine(checkSetGet.GetFractionString());
        Console.WriteLine(checkSetGet.GetDecimalValue());

        Random r = new Random();
        Fraction practice = new Fraction();

        for (int i = 0; i < 20; i++)
        {
            int t = r.Next(1, 11);
            int b = r.Next(1, 11);
            practice.SetTop(t);
            practice.SetBottom(b);

            Console.Write($"Fraction {i + 1}: ");
            Console.Write($"string: {practice.GetFractionString()}");
            Console.WriteLine($" Number: {practice.GetDecimalValue()}");
        }
    }
}