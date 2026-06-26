using System;

class Building
{
    private string _name;
    private int _currentTier;
    private int _baseCost;
    private int _bonusPerTier;
    private string _bonusDescription;

    public Building(string name, int baseCost, int bonusPerTier, string bonusDescription)
    {
        _name = name;
        _currentTier = 0;
        _baseCost = baseCost;
        _bonusPerTier = bonusPerTier;
        _bonusDescription = bonusDescription;
    }

    public string GetName() => _name;
    public int GetCurrentTier() => _currentTier;
    public int GetTotalBonus() => _currentTier * _bonusPerTier;
    public int GetNextCost() => _baseCost + (_currentTier * _baseCost);
    public void SetTier(int tier) => _currentTier = Math.Max(0, tier);
    public string Encode() => $"{_name}:{_currentTier}";

    public void Upgrade() => _currentTier++;

    public string GetStatusString()
    {
        string s = $"{_name} (Tier {_currentTier})";
        s += $"\n   {_bonusDescription}";
        s += $"\n   Current bonus: +{GetTotalBonus()}%";
        s += $"\n   Next upgrade (Tier {_currentTier + 1}): {GetNextCost()} gold";
        return s;
    }
}