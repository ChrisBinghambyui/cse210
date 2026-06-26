using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int reward, GoalType type = GoalType.General)
        : base(name, description, reward, type) { }

    public override int RecordEvent() => GetReward();
    public override bool IsComplete() => false;
    public override string GetStatus() => $"[ ] {GetName()} - {GetDescription()} ({GetGoalType()}) [eternal]";
    public override string Encode() => $"Eternal|{GetName()}|{GetDescription()}|{GetReward()}|{GetGoalType()}";
}
