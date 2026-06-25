using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int reward, GoalType type = GoalType.General)
        : base(name, description, reward, type) { }

    public override int RecordEvent() { return GetReward(); }
    public override bool IsComplete() { return false; }

    public override string GetStatus()
    {
        return "[ ] " + GetName() + " - " + GetDescription() + " (" + GetGoalType() + ") [eternal]";
    }

    public override string Encode()
    {
        return "Eternal|" + GetName() + "|" + GetDescription() + "|" + GetReward() + "|" + GetGoalType();
    }
}
