using System;
using System.Collections.Generic;

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

    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public int GetReward() { return _reward; }
    public GoalType GetGoalType() { return _type; }

    public virtual int RecordEvent() { return _reward; }
    public virtual bool IsComplete() { return false; }

    public virtual string GetStatus()
    {
        return "[ ] " + _name + " (" + _type + ")";
    }

    public virtual string Encode()
    {
        return "Goal|" + _name + "|" + _description + "|" + _reward + "|" + _type;
    }
}
