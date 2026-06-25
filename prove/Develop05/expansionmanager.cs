using System;
using System.Collections.Generic;

class ExpansionManager
{
    private int _expansionTier;
    private List<Quarter> _quarters;
    private int _expansionCost;

    public ExpansionManager()
    {
        _expansionTier = 0;
        _quarters = new List<Quarter>();
        _expansionCost = 100;
    }

    public ExpansionManager(int tier, List<Quarter> quarters)
    {
        _expansionTier = tier;
        _quarters = quarters;
        _expansionCost = 100 + (tier * 75);
    }

    public int GetExpansionTier() { return _expansionTier; }
    public List<Quarter> GetQuarters() { return _quarters; }
    public int GetExpansionCost() { return _expansionCost; }

    public int GetVillagerCapacityBonus() { return _expansionTier * 2; }
    public int GetCombatPowerBonus() { return _expansionTier * 5; }

    public int GetBonusForSpecialty(QuarterSpecialty specialty)
    {
        int total = 0;
        foreach (Quarter q in _quarters)
        {
            if (q.GetSpecialty() == specialty)
            {
                total += q.GetBonusValue();
            }
        }
        return total;
    }

    public void ExpandPaid()
    {
        _expansionTier++;
        _expansionCost = 100 + (_expansionTier * 75);

        Quarter newQuarter = Quarter.Generate(_expansionTier);
        _quarters.Add(newQuarter);

        Console.WriteLine("Your village expands! A new district takes shape:");
        Console.WriteLine(newQuarter.GetStatusString());
    }

    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine("=== Village Districts (Tier " + _expansionTier + ") ===");
        Console.WriteLine("Next expansion cost: " + _expansionCost + " gold");
        if (_quarters.Count == 0)
        {
            Console.WriteLine("No districts yet. Expand your village to unlock new quarters.");
            return;
        }
        foreach (Quarter q in _quarters)
        {
            Console.WriteLine("- " + q.GetStatusString());
            Console.WriteLine();
        }
    }

    public string Encode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(_expansionTier.ToString());
        sb.AppendLine(_quarters.Count.ToString());
        foreach (Quarter q in _quarters)
        {
            sb.AppendLine(q.Encode());
        }
        return sb.ToString().TrimEnd();
    }
}
