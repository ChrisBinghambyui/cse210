using System;
using System.Collections.Generic;

class Listing : Activity
{
    private List<string> _promptList;
    private int _responseCounter;
    private static List<string> _usedPromptList = new List<string>();

    public Listing()
        : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _promptList = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _responseCounter = 0;
    }

    public void RunActivity()
    {
        DisplayStartMessage();

        PrintPrompt();

        Console.WriteLine();
        Console.WriteLine("You may begin in:");
        PauseTimerCountdown(5);

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.Write("> ");
            string response = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(response))
            {
                IncreaseCount();
            }
        }

        Console.WriteLine();
        PrintCount();

        DisplayEndMessage();
    }

    private string ChoosePrompt()
    {
        if (_usedPromptList.Count >= _promptList.Count)
        {
            _usedPromptList.Clear();
        }

        List<string> available = new List<string>();

        for (int i = 0; i < _promptList.Count; i++)
        {
            if (!_usedPromptList.Contains(_promptList[i]))
            {
                available.Add(_promptList[i]);
            }
        }

        Random random = new Random();
        int index = random.Next(available.Count);
        string prompt = available[index];
        _usedPromptList.Add(prompt);

        return prompt;
    }

    public void PrintPrompt()
    {
        Console.WriteLine();
        Console.WriteLine(ChoosePrompt());
    }

    public void IncreaseCount()
    {
        _responseCounter++;
    }

    public void PrintCount()
    {
        Console.WriteLine($"You listed {_responseCounter} items!");
    }
}