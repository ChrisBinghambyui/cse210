using System;
using System.Collections.Generic;

class CommissionBoard
{
    private List<Commission> _dailyCommissions;
    private List<Commission> _weeklyCommissions;
    private DateTime _lastDailyRefresh;
    private DateTime _lastWeeklyRefresh;
    private int _dailySetBonus;
    private int _weeklySetBonus;

    private static List<string> _dailyPool = new List<string>
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

    private static List<string> _weeklyPool = new List<string>
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
        _dailySetBonus = 75;
        _weeklySetBonus = 200;
    }

    public CommissionBoard(List<Commission> daily, List<Commission> weekly, DateTime lastDaily, DateTime lastWeekly)
    {
        _dailyCommissions = daily;
        _weeklyCommissions = weekly;
        _lastDailyRefresh = lastDaily;
        _lastWeeklyRefresh = lastWeekly;
        _dailySetBonus = 75;
        _weeklySetBonus = 200;
    }

    public void Refresh(List<string> villagerNames)
    {
        DateTime now = DateTime.Now;

        if ((now - _lastDailyRefresh).TotalHours >= 24)
        {
            _dailyCommissions.Clear();
            _lastDailyRefresh = now;
            DateTime dailyDeadline = now.AddHours(24);
            Random rng = new Random();
            List<string> pool = new List<string>(_dailyPool);
            int count = Math.Min(4, pool.Count);
            for (int i = 0; i < count; i++)
            {
                int idx = rng.Next(pool.Count);
                string[] parts = pool[idx].Split("|");
                string villager = villagerNames.Count > 0 ? villagerNames[rng.Next(villagerNames.Count)] : "Notice Board";
                GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[2]);
                _dailyCommissions.Add(new Commission(parts[0], parts[1], int.Parse(parts[3]),
                    int.Parse(parts[4]), type, CommissionFrequency.Daily, dailyDeadline, villager));
                pool.RemoveAt(idx);
            }
        }

        if ((now - _lastWeeklyRefresh).TotalDays >= 7)
        {
            _weeklyCommissions.Clear();
            _lastWeeklyRefresh = now;
            DateTime weeklyDeadline = now.AddDays(7);
            Random rng = new Random();
            List<string> pool = new List<string>(_weeklyPool);
            int count = Math.Min(3, pool.Count);
            for (int i = 0; i < count; i++)
            {
                int idx = rng.Next(pool.Count);
                string[] parts = pool[idx].Split("|");
                string villager = villagerNames.Count > 0 ? villagerNames[rng.Next(villagerNames.Count)] : "Notice Board";
                GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[2]);
                _weeklyCommissions.Add(new Commission(parts[0], parts[1], int.Parse(parts[3]),
                    int.Parse(parts[4]), type, CommissionFrequency.Weekly, weeklyDeadline, villager));
                pool.RemoveAt(idx);
            }
        }
    }

    public List<Commission> GetAllActive()
    {
        List<Commission> all = new List<Commission>();
        all.AddRange(_dailyCommissions);
        all.AddRange(_weeklyCommissions);
        return all;
    }

    public bool CheckDailySetComplete()
    {
        foreach (Commission c in _dailyCommissions)
        {
            if (!c.IsComplete()) return false;
        }
        return _dailyCommissions.Count > 0;
    }

    public bool CheckWeeklySetComplete()
    {
        foreach (Commission c in _weeklyCommissions)
        {
            if (!c.IsComplete()) return false;
        }
        return _weeklyCommissions.Count > 0;
    }

    public int GetDailySetBonus() { return _dailySetBonus; }
    public int GetWeeklySetBonus() { return _weeklySetBonus; }
    public DateTime GetLastDailyRefresh() { return _lastDailyRefresh; }
    public DateTime GetLastWeeklyRefresh() { return _lastWeeklyRefresh; }

    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine("=== Missives and Errands ===");
        Console.WriteLine("-- Daily (bonus for full set: +" + _dailySetBonus + " XP, +" + _dailySetBonus / 2 + " gold) --");
        if (_dailyCommissions.Count == 0)
        {
            Console.WriteLine("  None posted yet.");
        }
        else
        {
            for (int i = 0; i < _dailyCommissions.Count; i++)
            {
                Console.WriteLine("  " + (i + 1) + ". " + _dailyCommissions[i].GetStatus());
            }
        }
        Console.WriteLine();
        Console.WriteLine("-- Weekly (bonus for full set: +" + _weeklySetBonus + " XP, +" + _weeklySetBonus / 2 + " gold) --");
        if (_weeklyCommissions.Count == 0)
        {
            Console.WriteLine("  None posted yet.");
        }
        else
        {
            for (int i = 0; i < _weeklyCommissions.Count; i++)
            {
                Console.WriteLine("  " + (i + _dailyCommissions.Count + 1) + ". " + _weeklyCommissions[i].GetStatus());
            }
        }
    }

    public Commission GetCommissionByIndex(int index)
    {
        List<Commission> all = GetAllActive();
        if (index < 0 || index >= all.Count) return null;
        return all[index];
    }

    public string Encode()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine(_lastDailyRefresh.Ticks.ToString());
        sb.AppendLine(_lastWeeklyRefresh.Ticks.ToString());
        sb.AppendLine(_dailyCommissions.Count.ToString());
        foreach (Commission c in _dailyCommissions)
        {
            sb.AppendLine(c.Encode());
        }
        sb.AppendLine(_weeklyCommissions.Count.ToString());
        foreach (Commission c in _weeklyCommissions)
        {
            sb.AppendLine(c.Encode());
        }
        return sb.ToString().TrimEnd();
    }
}
