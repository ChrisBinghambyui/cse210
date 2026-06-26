using System;

class ChecklistGoal : Goal
{
    private int _timesComplete;
    private int _limitCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int reward, int limitCount, int bonusPoints,
        GoalType type = GoalType.General, int timesComplete = 0)
        : base(name, description, reward, type)
    {
        _timesComplete = timesComplete;
        _limitCount = limitCount;
        _bonusPoints = bonusPoints;
    }

    public override bool IsComplete() => _timesComplete >= _limitCount;
    public string GetProgress() => $"{_timesComplete}/{_limitCount}";

    public override int RecordEvent()
    {
        if (IsComplete()) { Console.WriteLine("This goal is already complete."); return 0; }
        _timesComplete++;
        int earned = GetReward();
        if (_timesComplete == _limitCount)
        {
            earned += _bonusPoints;
            Console.WriteLine($"Checklist complete! Bonus: +{_bonusPoints} XP!");
        }
        return earned;
    }

    public override string GetStatus()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        return $"{box} {GetName()} - {GetDescription()} ({GetGoalType()}) [{GetProgress()}]";
    }

    public override string Encode() =>
        $"Checklist|{GetName()}|{GetDescription()}|{GetReward()}|{GetGoalType()}|{_limitCount}|{_bonusPoints}|{_timesComplete}";
}
