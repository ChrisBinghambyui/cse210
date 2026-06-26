using System;
using System.Collections.Generic;

class ExpansionManager
{
    private int _expansionTier;
    private List<Quarter> _quarters;
    private int _expansionCost;

    public ExpansionManager() : this(0, new List<Quarter>()) { }

    public ExpansionManager(int tier, List<Quarter> quarters)
    {
        _expansionTier = tier;
        _quarters = quarters;
        _expansionCost = 100 + (tier * 75);
    }

    public int GetExpansionTier() => _expansionTier;
    public List<Quarter> GetQuarters() => _quarters;
    public int GetExpansionCost() => _expansionCost;
    public int GetVillagerCapacityBonus() => _expansionTier * 2;
    public int GetCombatPowerBonus() => _expansionTier * 5;

    public int GetBonusForSpecialty(QuarterSpecialty specialty)
    {
        int total = 0;
        foreach (Quarter q in _quarters)
            if (q.GetSpecialty() == specialty) total += q.GetBonusValue();
        return total;
    }

    public void ExpandPaid()
    {
        _expansionTier++;
        _expansionCost = 100 + (_expansionTier * 75);
        Quarter newQuarter = Quarter.Generate(_expansionTier);
        _quarters.Add(newQuarter);
        Console.WriteLine($"Your village expands! A new district takes shape:\n{newQuarter.GetStatusString()}");
    }

    public void Display()
    {
        Console.WriteLine($"\n=== Village Districts (Tier {_expansionTier}) ===");
        Console.WriteLine($"Next expansion cost: {_expansionCost} gold");
        if (_quarters.Count == 0) { Console.WriteLine("No districts yet."); return; }
        foreach (Quarter q in _quarters)
            Console.WriteLine($"- {q.GetStatusString()}\n");
    }

    public string Encode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(_expansionTier.ToString());
        sb.AppendLine(_quarters.Count.ToString());
        foreach (Quarter q in _quarters) sb.AppendLine(q.Encode());
        return sb.ToString().TrimEnd();
    }
}
