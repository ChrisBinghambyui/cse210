using System;

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int reward, GoalType type = GoalType.General, bool isComplete = false)
        : base(name, description, reward, type)
    {
        _isComplete = isComplete;
    }

    public override bool IsComplete() => _isComplete;

    public override int RecordEvent()
    {
        if (_isComplete) { Console.WriteLine("This goal is already complete."); return 0; }
        _isComplete = true;
        return GetReward();
    }

    public override string GetStatus()
    {
        string box = _isComplete ? "[X]" : "[ ]";
        return $"{box} {GetName()} - {GetDescription()} ({GetGoalType()})";
    }

    public override string Encode() =>
        $"Simple|{GetName()}|{GetDescription()}|{GetReward()}|{GetGoalType()}|{_isComplete}";
}
