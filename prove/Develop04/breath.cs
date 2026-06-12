using System;

class Breathing : Activity
{
    private bool _inOrOut;

    public Breathing()
        : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void RunActivity()
    {
        DisplayStartMessage();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        _inOrOut = true;

        while (DateTime.Now < endTime)
        {
            DisplayBreatheMessage(_inOrOut);
            PauseTimerCountdown(4);
            _inOrOut = InOrOut();
        }

        DisplayEndMessage();
    }

    private string BreatheIn()
    {
        return "Breathe in...";
    }

    private string BreatheOut()
    {
        return "Breathe out...";
    }

    private bool InOrOut()
    {
        return !_inOrOut;
    }

    private void DisplayBreatheMessage(bool inOrOut)
    {
        Console.WriteLine();

        if (inOrOut)
        {
            Console.WriteLine(BreatheIn());
        }
        else
        {
            Console.WriteLine(BreatheOut());
        }
    }
}