using System;

class Building
{
    private string _name;
    private int _currentTier;
    private int _maxTier;
    private string[] _tierNames;
    private string[] _tierDescriptions;
    private int[] _tierCosts;
    private int[] _tierBonuses;

    public Building(string name, string[] tierNames, string[] tierDescriptions, int[] tierCosts, int[] tierBonuses)
    {
        _name = name;
        _currentTier = 0;
        _maxTier = tierCosts.Length;
        _tierNames = tierNames;
        _tierDescriptions = tierDescriptions;
        _tierCosts = tierCosts;
        _tierBonuses = tierBonuses;
    }

    public string GetName() { return _name; }
    public int GetCurrentTier() { return _currentTier; }
    public int GetMaxTier() { return _maxTier; }
    public bool IsMaxed() { return _currentTier >= _maxTier; }

    public int GetNextCost()
    {
        if (IsMaxed()) return 0;
        return _tierCosts[_currentTier];
    }

    public int GetTotalBonus()
    {
        int total = 0;
        for (int i = 0; i < _currentTier; i++) total += _tierBonuses[i];
        return total;
    }

    public bool Upgrade()
    {
        if (IsMaxed()) return false;
        _currentTier++;
        return true;
    }

    public void SetTier(int tier) { _currentTier = Math.Min(tier, _maxTier); }

    public string GetStatusString()
    {
        string s = _name + " (Tier " + _currentTier + "/" + _maxTier + ")";
        if (_currentTier > 0)
        {
            s += "\n   " + _tierNames[_currentTier - 1] + ": " + _tierDescriptions[_currentTier - 1];
            s += "\n   Current bonus: +" + GetTotalBonus() + "%";
        }
        if (!IsMaxed())
            s += "\n   Next: " + _tierNames[_currentTier] + " - " + GetNextCost() + " gold";
        else
            s += "\n   Fully upgraded!";
        return s;
    }

    public string Encode() { return _name + ":" + _currentTier; }
}
