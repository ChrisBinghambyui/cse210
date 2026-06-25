using System;
using System.Threading;

class Activity
{
    private string _name;
    private string _description;
    private int _durationTime;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartMessage()
    {
        Console.WriteLine();
        Console.WriteLine($"Welcome to the {_name}.");
        Console.WriteLine();
        DisplayDescription();
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");
        int time = int.Parse(Console.ReadLine());
        SetDuration(time);

        Console.WriteLine();
        Console.WriteLine("Get ready...");
        PauseTimerSpin(3);
    }

    public void DisplayEndMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        PauseTimerSpin(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed another {GetDuration()} seconds of the {_name}.");
        PauseTimerSpin(3);
    }

    public int GetDuration()
    {
        return _durationTime;
    }

    public void SetDuration(int time)
    {
        _durationTime = time;
    }

    public void PauseTimerSpin(int time)
    {
        string[] spinner = { "/", "-", "\\", "|" };
        int ticks = time * 4;

        for (int i = 0; i < ticks; i++)
        {
            Console.Write(spinner[i % spinner.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }

    public void PauseTimerCountdown(int time)
    {
        for (int i = time; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b");

            if (i >= 10)
            {
                Console.Write("\b");
            }
        }
    }

    private string GetDescription()
    {
        return _description;
    }

    public void SetDescription(string description)
    {
        _description = description;
    }

    public void DisplayDescription()
    {
        Console.WriteLine(GetDescription());
    }
}