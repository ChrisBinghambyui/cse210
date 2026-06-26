using System;

enum GoalType { Physical, Social, Educational, Spiritual, General }

class Goal
{
    private string _name;
    private string _description;
    private int _reward;
    private GoalType _type;

    public Goal(string name, string description, int reward, GoalType type = GoalType.General)
    {
        _name = name;
        _description = description;
        _reward = reward;
        _type = type;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetReward() => _reward;
    public GoalType GetGoalType() => _type;

    public virtual int RecordEvent() => _reward;
    public virtual bool IsComplete() => false;
    public virtual string GetStatus() => $"[ ] {_name} ({_type})";
    public virtual string Encode() => $"Goal|{_name}|{_description}|{_reward}|{_type}";
}
