using System;

class ChecklistGoal : Goal
{
    private int _timesComplete;
    private int _limitCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int reward, int limitCount, int bonusPoints, GoalType type = GoalType.General)
        : base(name, description, reward, type)
    {
        _timesComplete = 0;
        _limitCount = limitCount;
        _bonusPoints = bonusPoints;
    }

    public ChecklistGoal(string name, string description, int reward, int limitCount, int bonusPoints, int timesComplete, GoalType type = GoalType.General)
        : base(name, description, reward, type)
    {
        _timesComplete = timesComplete;
        _limitCount = limitCount;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            Console.WriteLine("This goal is already complete.");
            return 0;
        }
        _timesComplete++;
        int earned = GetReward();
        if (_timesComplete == _limitCount)
        {
            earned += _bonusPoints;
            Console.WriteLine("Checklist complete! Bonus: +" + _bonusPoints + " XP!");
        }
        return earned;
    }

    public override bool IsComplete() { return _timesComplete >= _limitCount; }

    public string GetProgress() { return _timesComplete + "/" + _limitCount; }

    public override string GetStatus()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        return box + " " + GetName() + " - " + GetDescription() + " (" + GetGoalType() + ") [" + GetProgress() + "]";
    }

    public override string Encode()
    {
        return "Checklist|" + GetName() + "|" + GetDescription() + "|" + GetReward() + "|" + GetGoalType() + "|" + _limitCount + "|" + _bonusPoints + "|" + _timesComplete;
    }
}
