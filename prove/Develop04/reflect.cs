using System;
using System.Collections.Generic;

class Reflection : Activity
{
    private List<string> _promptList;
    private List<string> _questionList;
    private List<string> _usedQuestionList;
    private Random _random;
    private static List<string> _usedPromptList = new List<string>();

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

        _usedQuestionList = new List<string>();
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

        int index = _random.Next(available.Count);
        string prompt = available[index];
        _usedPromptList.Add(prompt);

        return prompt;
    }

    private void DisplayPrompt()
    {
        Console.WriteLine();
        Console.WriteLine(ChoosePrompt());
    }

    private string ChooseQuestion()
    {
        if (_usedQuestionList.Count >= _questionList.Count)
        {
            _usedQuestionList.Clear();
        }

        List<string> available = new List<string>();

        for (int i = 0; i < _questionList.Count; i++)
        {
            if (!_usedQuestionList.Contains(_questionList[i]))
            {
                available.Add(_questionList[i]);
            }
        }

        int index = _random.Next(available.Count);
        string question = available[index];
        _usedQuestionList.Add(question);

        return question;
    }

    private void DisplayQuestion()
    {
        Console.WriteLine();
        Console.Write(ChooseQuestion());
    }
}