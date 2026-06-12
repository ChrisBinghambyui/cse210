using System;
using System.Collections.Generic;

class Reflection : Activity
{
    private List<string> _promptList;
    private List<string> _questionList;
    private Random _random;

    public Reflection()
        : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        _random = new Random();

        _promptList = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questionList = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public void RunActivity()
    {
        DisplayStartMessage();

        DisplayPrompt();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            DisplayQuestion();
            PauseTimerSpin(5);
        }

        DisplayEndMessage();
    }

    private string ChoosePrompt()
    {
        int index = _random.Next(_promptList.Count);
        return _promptList[index];
    }

    private void DisplayPrompt()
    {
        Console.WriteLine();
        Console.WriteLine(ChoosePrompt());
    }

    private string ChooseQuestion()
    {
        int index = _random.Next(_questionList.Count);
        return _questionList[index];
    }

    private void DisplayQuestion()
    {
        Console.WriteLine();
        Console.Write(ChooseQuestion());
    }
}