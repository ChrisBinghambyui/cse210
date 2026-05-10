using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private List<string> _prompts;
    public void Add(string prompt)
    {
        _prompts.Add(prompt);
    }

    public PromptGenerator()
    {
        _prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "Who did I spend the most time with today?",
            "What is the last dream I can remember having?",
            "If I could make any single purchase and have it wholly paid for, no questions asked, what would I buy?",
            "What is the best thing I ate today?",
            "What was the last scripture I studied about?"
        };
    }

    public string GetRandomPrompt()
    {
        if (_prompts == null || _prompts.Count == 0)
        {
            return "No prompts available.";
        }

        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}
