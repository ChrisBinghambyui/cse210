using System;
using System.Collections.Generic;

class VillagerManager
{
    private List<Villager> _villagers;
    private int _maxVillagers;

    public VillagerManager()
    {
        _villagers = new List<Villager>();
        _maxVillagers = 3;
    }

    public VillagerManager(List<Villager> villagers, int maxVillagers)
    {
        _villagers = villagers;
        _maxVillagers = maxVillagers;
    }

    public List<Villager> GetVillagers() { return _villagers; }
    public int GetCount() { return _villagers.Count; }
    public int GetMaxVillagers() { return _maxVillagers; }
    public void IncreaseCapacity(int amount) { _maxVillagers += amount; }

    public bool RecruitVillager()
    {
        if (_villagers.Count >= _maxVillagers)
        {
            Console.WriteLine("Your village is at capacity. Expand to recruit more.");
            return false;
        }
        Villager v = Villager.GenerateRandom();
        _villagers.Add(v);
        Console.WriteLine(v.GetFullName() + " the " + v.GetTitle() + " has joined your village!");
        return true;
    }

    public List<string> GetVillagerNames()
    {
        List<string> names = new List<string>();
        foreach (Villager v in _villagers)
        {
            names.Add(v.GetFullName());
        }
        return names;
    }

    public float GetSpecialtyBonus(VillagerSpecialty specialty)
    {
        float total = 0;
        foreach (Villager v in _villagers)
        {
            if (v.GetSpecialty() == specialty)
            {
                total += v.GetContributionMultiplier();
            }
        }
        return total;
    }

    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine("=== Villagers (" + _villagers.Count + "/" + _maxVillagers + ") ===");
        if (_villagers.Count == 0)
        {
            Console.WriteLine("No villagers yet. Recruit from the Tavern.");
            return;
        }
        for (int i = 0; i < _villagers.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + _villagers[i].GetStatusString());
            Console.WriteLine();
        }
    }

    public Commission InteractWithVillager(int index)
    {
        if (index < 0 || index >= _villagers.Count)
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }

        Villager v = _villagers[index];
        Console.WriteLine();
        Console.WriteLine("You speak with " + v.GetFullName() + ", " + v.GetTitle() + ".");

        if (v.CanOfferQuest() && !v.IsQuestComplete())
        {
            Console.WriteLine(v.GetFullName() + " has a personal quest for you.");
            Commission quest = v.GeneratePersonalQuest();
            Console.WriteLine("Quest: " + quest.GetName());
            Console.WriteLine("  " + quest.GetDescription());
            Console.WriteLine("  Reward: +" + quest.GetReward() + " XP, +" + quest.GetGoldReward() + " gold");
            Console.WriteLine("  No time limit. Complete it whenever you are ready.");
            return quest;
        }
        else if (v.IsQuestComplete())
        {
            Console.WriteLine("You have already completed " + v.GetFullName() + "'s personal quest. They nod with pride.");
        }
        else
        {
            int daysLeft = 7 - (int)(DateTime.Now - v.GetVillagerArrivalDate()).TotalDays;
            Console.WriteLine(v.GetFullName() + " is still settling in. Check back in about " + Math.Max(0, daysLeft) + " day(s).");
        }
        return null;
    }

    public void CompleteVillagerQuest(Villager v)
    {
        string oldTitle = v.GetTitle();
        v.CompleteQuest();
        Console.WriteLine(v.GetFullName() + " has grown! " + oldTitle + " -> " + v.GetTitle());
        Console.WriteLine("Their contribution to the village has increased.");
    }

    public string Encode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(_maxVillagers.ToString());
        sb.AppendLine(_villagers.Count.ToString());
        foreach (Villager v in _villagers)
        {
            sb.AppendLine(v.Encode());
        }
        return sb.ToString().TrimEnd();
    }
}
