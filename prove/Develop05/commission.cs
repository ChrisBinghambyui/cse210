using System;

enum CommissionFrequency { Daily, Weekly }

class Commission : Goal
{
    private CommissionFrequency _frequency;
    private int _goldReward;
    private bool _isComplete;
    private DateTime _deadline;
    private string _villagerSource;

    public Commission(string name, string description, int xpReward, int goldReward,
        GoalType type, CommissionFrequency frequency, DateTime deadline, string villagerSource = "Notice Board")
        : base(name, description, xpReward, type)
    {
        _goldReward = goldReward;
        _frequency = frequency;
        _isComplete = false;
        _deadline = deadline;
        _villagerSource = villagerSource;
    }

    public int GetGoldReward() { return _goldReward; }
    public CommissionFrequency GetFrequency() { return _frequency; }
    public DateTime GetDeadline() { return _deadline; }
    public string GetVillagerSource() { return _villagerSource; }
    public bool IsExpired() { return DateTime.Now > _deadline && !_isComplete; }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("Already completed.");
            return 0;
        }
        if (IsExpired())
        {
            Console.WriteLine("This commission has expired.");
            return 0;
        }
        _isComplete = true;
        return GetReward();
    }

    public override bool IsComplete() { return _isComplete; }

    public override string GetStatus()
    {
        string box = _isComplete ? "[X]" : (IsExpired() ? "[!]" : "[ ]");
        string due = _isComplete ? "done" : (IsExpired() ? "EXPIRED" : "due " + _deadline.ToString("MMM dd HH:mm"));
        return box + " [" + _frequency + "] " + GetName() + " (" + GetGoalType() + ") - " + due
            + " | +" + GetReward() + " XP, +" + _goldReward + " gold | from: " + _villagerSource;
    }

    public override string Encode()
    {
        return "Commission|" + GetName() + "|" + GetDescription() + "|" + GetReward() + "|" + GetGoalType()
            + "|" + _goldReward + "|" + _frequency + "|" + _deadline.Ticks + "|" + _isComplete + "|" + _villagerSource;
    }
}
