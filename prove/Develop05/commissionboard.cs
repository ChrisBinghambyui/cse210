using System;
using System.Collections.Generic;

class CommissionBoard
{
    private List<Commission> _dailyCommissions;
    private List<Commission> _weeklyCommissions;
    private DateTime _lastDailyRefresh;
    private DateTime _lastWeeklyRefresh;

    private const int DailySetBonus = 75;
    private const int WeeklySetBonus = 200;

    private static string[] _dailyPool =
    {
        "Go on a 15-minute walk|Walk outside for at least 15 minutes|Physical|30|15",
        "Drink 8 glasses of water|Stay hydrated throughout the day|Physical|20|10",
        "Read for 30 minutes|Read a book or scripture for at least 30 minutes|Educational|30|15",
        "Journal for 10 minutes|Write in your journal for at least 10 minutes|Spiritual|25|12",
        "Meditate for 5 minutes|Take 5 minutes to be still and reflect|Spiritual|20|10",
        "Call a friend or family member|Reach out and connect with someone you care about|Social|25|12",
        "Do 20 minutes of stretching|Stretch or do light yoga|Physical|20|10",
        "Cook a meal from scratch|Prepare a full meal without shortcuts|Physical|30|15",
        "Write down 3 things you are grateful for|Practice gratitude in writing|Spiritual|20|10",
        "Go to bed before 11pm|Prioritize a good night of rest|Physical|20|10",
        "Spend 20 minutes tidying your space|Clean and organize your living area|Social|20|10",
        "Do something kind for someone|An act of service, big or small|Social|30|15"
    };

    private static string[] _weeklyPool =
    {
        "Work out for a total of 3 hours|Accumulate 3 hours of exercise this week|Physical|120|60",
        "Try 3 new recipes|Cook three meals you have never made before|Physical|100|50",
        "Read an entire book|Finish a complete book this week|Educational|150|75",
        "Attend a social event|Go to a gathering, activity, or meetup|Social|100|50",
        "Study for 5 hours total|Put in at least 5 hours of focused study|Educational|150|75",
        "Complete 5 acts of service|Do something for others on 5 separate occasions|Social|120|60",
        "Attend all your Sunday meetings|Be present for all church or religious meetings|Spiritual|150|75",
        "Study scripture every day this week|Read scripture on all 7 days|Spiritual|150|75",
        "Spend less than 2 hours on social media total|Limit your screen time this week|Educational|100|50",
        "Write 3 journal entries|Journal at least 3 times this week|Spiritual|100|50"
    };

    public CommissionBoard()
    {
        _dailyCommissions = new List<Commission>();
        _weeklyCommissions = new List<Commission>();
        _lastDailyRefresh = DateTime.MinValue;
        _lastWeeklyRefresh = DateTime.MinValue;
    }

    public CommissionBoard(List<Commission> daily, List<Commission> weekly, DateTime lastDaily, DateTime lastWeekly)
    {
        _dailyCommissions = daily;
        _weeklyCommissions = weekly;
        _lastDailyRefresh = lastDaily;
        _lastWeeklyRefresh = lastWeekly;
    }

    public DateTime GetLastDailyRefresh() => _lastDailyRefresh;
    public DateTime GetLastWeeklyRefresh() => _lastWeeklyRefresh;
    public int GetDailySetBonus() => DailySetBonus;
    public int GetWeeklySetBonus() => WeeklySetBonus;

    public void Refresh(List<string> villagerNames)
    {
        DateTime now = DateTime.Now;
        if ((now - _lastDailyRefresh).TotalHours >= 24)
        {
            _dailyCommissions = GeneratePool(_dailyPool, 4, now.AddHours(24), villagerNames, CommissionFrequency.Daily);
            _lastDailyRefresh = now;
        }
        if ((now - _lastWeeklyRefresh).TotalDays >= 7)
        {
            _weeklyCommissions = GeneratePool(_weeklyPool, 3, now.AddDays(7), villagerNames, CommissionFrequency.Weekly);
            _lastWeeklyRefresh = now;
        }
    }

    private List<Commission> GeneratePool(string[] pool, int count, DateTime deadline, List<string> villagerNames, CommissionFrequency frequency)
    {
        Random rng = new Random();
        List<string> remaining = new List<string>(pool);
        List<Commission> result = new List<Commission>();

        for (int i = 0; i < Math.Min(count, remaining.Count); i++)
        {
            int idx = rng.Next(remaining.Count);
            string[] parts = remaining[idx].Split("|");
            string villager = villagerNames.Count > 0 ? villagerNames[rng.Next(villagerNames.Count)] : "Notice Board";
            GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[2]);
            result.Add(new Commission(parts[0], parts[1], int.Parse(parts[3]), int.Parse(parts[4]), type, frequency, deadline, villager));
            remaining.RemoveAt(idx);
        }
        return result;
    }

    public List<Commission> GetAllActive()
    {
        List<Commission> all = new List<Commission>(_dailyCommissions);
        all.AddRange(_weeklyCommissions);
        return all;
    }

    public Commission GetCommissionByIndex(int index)
    {
        List<Commission> all = GetAllActive();
        return (index >= 0 && index < all.Count) ? all[index] : null;
    }

    public bool CheckDailySetComplete()
    {
        if (_dailyCommissions.Count == 0) return false;
        foreach (Commission c in _dailyCommissions) if (!c.IsComplete()) return false;
        return true;
    }

    public bool CheckWeeklySetComplete()
    {
        if (_weeklyCommissions.Count == 0) return false;
        foreach (Commission c in _weeklyCommissions) if (!c.IsComplete()) return false;
        return true;
    }

    public void Display()
    {
        Console.WriteLine("\n=== Missives and Errands ===");
        DisplayPool("Daily", _dailyCommissions, DailySetBonus, 1);
        DisplayPool("Weekly", _weeklyCommissions, WeeklySetBonus, _dailyCommissions.Count + 1);
    }

    private void DisplayPool(string label, List<Commission> pool, int setBonus, int startIndex)
    {
        Console.WriteLine($"\n-- {label} (full set bonus: +{setBonus} XP, +{setBonus / 2} gold) --");
        if (pool.Count == 0) { Console.WriteLine("  None posted yet."); return; }
        for (int i = 0; i < pool.Count; i++)
            Console.WriteLine($"  {startIndex + i}. {pool[i].GetStatus()}");
    }

    public string Encode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(_lastDailyRefresh.Ticks.ToString());
        sb.AppendLine(_lastWeeklyRefresh.Ticks.ToString());
        sb.AppendLine(_dailyCommissions.Count.ToString());
        foreach (Commission c in _dailyCommissions) sb.AppendLine(c.Encode());
        sb.AppendLine(_weeklyCommissions.Count.ToString());
        foreach (Commission c in _weeklyCommissions) sb.AppendLine(c.Encode());
        return sb.ToString().TrimEnd();
    }
}
