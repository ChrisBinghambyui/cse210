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
        {
            _buildings[i].SetTier(tiers[i]);
        }
    }

    private void InitBuildings()
    {
        _buildings.Add(new Building(
            "Town Hall",
            new string[] { "Tax Ledger", "Royal Charter", "Grand Treasury" },
            new string[]
            {
                "A scribe tallies every coin. Upgrade costs reduced 10%.",
                "The king's seal legitimizes trade. Costs reduced 20% total.",
                "A vault hums beneath the cobblestones. All gold gains up 30%."
            },
            new int[] { 50, 120, 250 },
            new int[] { 10, 10, 30 }
        ));

        _buildings.Add(new Building(
            "Blacksmith",
            new string[] { "Iron Works", "Tempered Steel", "Master Armory" },
            new string[]
            {
                "Swords sharpen. Simple goal rewards up 25%. Village damage up 5.",
                "Adventurers march out better armed. Simple goal rewards up 50% total. Damage up 10.",
                "The finest arms in the region. Simple goal rewards up 75% total. Damage up 20."
            },
            new int[] { 40, 100, 200 },
            new int[] { 25, 25, 25 }
        ));

        _buildings.Add(new Building(
            "Library",
            new string[] { "Apprentice Scrolls", "Arcane Annex", "The Grand Codex" },
            new string[]
            {
                "Dusty shelves fill with practice texts. Eternal goal rewards up 25%. Bypasses 3 enemy resistance.",
                "A restricted section opens. Eternal goal rewards up 50% total. Bypasses 8 resistance.",
                "Ancient knowledge flows freely. Eternal goal rewards up 75% total. Bypasses 15 resistance."
            },
            new int[] { 40, 100, 200 },
            new int[] { 25, 25, 25 }
        ));

        _buildings.Add(new Building(
            "Tavern",
            new string[] { "Open Tab", "Adventurers Guild", "The Legendary Round Table" },
            new string[]
            {
                "Wanderers drift in off the road. Checklist rewards up 25%. Recruit 1 extra villager.",
                "A proper guild board lines the wall. Checklist rewards up 50% total. Recruit 2 extra villagers.",
                "Heroes of renown answer the call. Checklist rewards up 75% total. Level-up gold doubled."
            },
            new int[] { 60, 140, 280 },
            new int[] { 25, 25, 25 }
        ));
    }

    public int GetGold() { return _gold; }
    public void AddGold(int amount) { _gold += amount; }
    public bool SpendGold(int amount)
    {
        if (_gold < amount) return false;
        _gold -= amount;
        return true;
    }

    public int GetBlacksmithBonus() { return _buildings[1].GetTotalBonus(); }
    public int GetLibraryBonus() { return _buildings[2].GetTotalBonus(); }
    public int GetTavernBonus() { return _buildings[3].GetTotalBonus(); }
    public int GetTownHallBonus() { return _buildings[0].GetTotalBonus(); }

    public int GetLibraryResistanceBypass()
    {
        int tier = _buildings[2].GetCurrentTier();
        if (tier == 0) return 0;
        if (tier == 1) return 3;
        if (tier == 2) return 8;
        return 15;
    }

    public int GetBlacksmithCombatBonus()
    {
        int tier = _buildings[1].GetCurrentTier();
        if (tier == 0) return 0;
        if (tier == 1) return 5;
        if (tier == 2) return 10;
        return 20;
    }

    public int GetGoldFromPoints(int points)
    {
        int base_ = points / 10;
        int bonus = GetTownHallBonus() > 0 ? base_ * GetTownHallBonus() / 100 : 0;
        return base_ + bonus;
    }

    private int GetDiscountedCost(int cost)
    {
        int discount = GetTownHallBonus();
        if (discount == 0) return cost;
        return cost - (cost * discount / 100);
    }

    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine("=== Village Buildings ===");
        Console.WriteLine("Gold: " + _gold);
        Console.WriteLine();
        for (int i = 0; i < _buildings.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + _buildings[i].GetStatusString());
            Console.WriteLine();
        }
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
        if (target.IsMaxed())
        {
            Console.WriteLine(target.GetName() + " is already fully upgraded.");
            return;
        }

        int cost = GetDiscountedCost(target.GetNextCost());
        Console.Write("Cost: " + cost + " gold. You have " + _gold + ". Confirm? (y/n): ");
        string confirm = Console.ReadLine();
        if (confirm != "y" && confirm != "Y") return;

        if (!SpendGold(cost))
        {
            Console.WriteLine("Not enough gold.");
            return;
        }
        target.Upgrade();
        Console.WriteLine(target.GetName() + " upgraded to tier " + target.GetCurrentTier() + "!");
    }

    public string Encode()
    {
        string result = _gold.ToString();
        foreach (Building b in _buildings)
        {
            result += "|" + b.GetCurrentTier();
        }
        return result;
    }
}
