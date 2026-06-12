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
            PauseTimerBreathe(_inOrOut, 4);
            _inOrOut = InOrOut();
        }

        DisplayEndMessage();
    }

    private void PauseTimerBreathe(bool inOrOut, int time)
    {
        for (int i = 1; i <= time; i++)
        {
            int length = inOrOut ? i : (time - i + 1);
            string bar = new string('-', length);

            Console.Write(bar);
            System.Threading.Thread.Sleep(1000);

            for (int j = 0; j < bar.Length; j++)
            {
                Console.Write("\b \b");
            }
        }
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