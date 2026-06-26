using System;
using System.Collections.Generic;

class TownManager
{
    private int _gold;
    private List<Building> _buildings;

    public TownManager()
    {
        _gold = 0;
        _buildings = new List<Building>();
        InitBuildings();
    }

    public TownManager(int gold, List<int> tiers)
    {
        _gold = gold;
        _buildings = new List<Building>();
        InitBuildings();
        for (int i = 0; i < tiers.Count && i < _buildings.Count; i++)
            _buildings[i].SetTier(tiers[i]);
    }

    private void InitBuildings()
    {
        _buildings.Add(new Building(
            "Town Hall",
            baseCost: 30,
            bonusPerTier: 5,
            "Each tier reduces upgrade costs and increases gold income by 5%."
        ));
        _buildings.Add(new Building(
            "Blacksmith",
            baseCost: 25,
            bonusPerTier: 10,
            "Each tier adds 10% to Simple goal XP rewards and increases village combat damage."
        ));
        _buildings.Add(new Building(
            "Library",
            baseCost: 25,
            bonusPerTier: 10,
            "Each tier adds 10% to Eternal goal XP rewards and bypasses more enemy resistance."
        ));
        _buildings.Add(new Building(
            "Tavern",
            baseCost: 25,
            bonusPerTier: 10,
            "Each tier adds 10% to Checklist goal XP rewards and improves level-up gold bonuses."
        ));
    }

    public int GetGold() => _gold;
    public void AddGold(int amount) => _gold += amount;

    public bool SpendGold(int amount)
    {
        if (_gold < amount) return false;
        _gold -= amount;
        return true;
    }

    public int GetBlacksmithBonus() => _buildings[1].GetTotalBonus();
    public int GetLibraryBonus() => _buildings[2].GetTotalBonus();
    public int GetTavernBonus() => _buildings[3].GetTotalBonus();
    public int GetTownHallBonus() => _buildings[0].GetTotalBonus();

    public int GetBlacksmithCombatBonus() => _buildings[1].GetCurrentTier() * 5;

    public int GetGoldFromPoints(int points)
    {
        int earned = points / 10;
        int bonus = GetTownHallBonus();
        return bonus > 0 ? earned + (earned * bonus / 100) : earned;
    }

    private int GetDiscountedCost(int cost)
    {
        int discount = GetTownHallBonus();
        return discount > 0 ? cost - (cost * discount / 100) : cost;
    }

    public void Display()
    {
        Console.WriteLine($"\n=== Village Buildings ===");
        Console.WriteLine($"Gold: {_gold}");
        Console.WriteLine();
        for (int i = 0; i < _buildings.Count; i++)
            Console.WriteLine($"{i + 1}. {_buildings[i].GetStatusString()}\n");
    }

    public void UpgradeMenu()
    {
        Display();
        Console.Write("Upgrade which building? (number, 0 to cancel): ");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        if (choice == 0) return;
        choice -= 1;

        if (choice < 0 || choice >= _buildings.Count)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Building target = _buildings[choice];
        int cost = GetDiscountedCost(target.GetNextCost());

        Console.Write($"Upgrade {target.GetName()} to tier {target.GetCurrentTier() + 1} for {cost} gold? You have {_gold}. (y/n): ");
        string confirm = Console.ReadLine();
        if (confirm != "y" && confirm != "Y") return;

        if (!SpendGold(cost))
        {
            Console.WriteLine("Not enough gold.");
            return;
        }

        target.Upgrade();
        Console.WriteLine($"{target.GetName()} upgraded to tier {target.GetCurrentTier()}! Bonus: +{target.GetTotalBonus()}%");
    }

    public string Encode()
    {
        string result = _gold.ToString();
        foreach (Building b in _buildings)
            result += "|" + b.GetCurrentTier();
        return result;
    }
}