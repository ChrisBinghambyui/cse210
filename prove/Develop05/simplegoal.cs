using System;

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int reward, GoalType type = GoalType.General)
        : base(name, description, reward, type)
    {
        _isComplete = false;
    }

    public SimpleGoal(string name, string description, int reward, GoalType type, bool isComplete)
        : base(name, description, reward, type)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("This goal is already complete.");
            return 0;
        }
        _isComplete = true;
        return GetReward();
    }

    public override bool IsComplete() { return _isComplete; }

    public override string GetStatus()
    {
        string box = _isComplete ? "[X]" : "[ ]";
        return box + " " + GetName() + " - " + GetDescription() + " (" + GetGoalType() + ")";
    }

    public override string Encode()
    {
        return "Simple|" + GetName() + "|" + GetDescription() + "|" + GetReward() + "|" + GetGoalType() + "|" + _isComplete;
    }
}
