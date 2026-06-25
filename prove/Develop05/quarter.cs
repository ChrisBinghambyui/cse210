using System;
using System.Collections.Generic;

enum QuarterSpecialty { Market, Industrial, Scholar, Clerical, Military, Artisan }

class Quarter
{
    private string _name;
    private QuarterSpecialty _specialty;
    private string _description;
    private int _bonusValue;

    private static List<string> _prefixes = new List<string>
    {
        "Ashfen", "Ironveil", "Goldmere", "Duskhollow", "Embervane", "Coldwater",
        "Thornwick", "Salthaven", "Coppergate", "Bramble", "Nightfall", "Stonebridge",
        "Silverbrook", "Rustholm", "Cindergate", "Mistwood", "Ironbell", "Greyveil"
    };

    private static List<string> _suffixes = new List<string>
    {
        "Row", "Quarter", "Ward", "District", "Cross", "Lane", "Gate",
        "Hollow", "Rise", "Reach", "Passage", "Common", "Green", "Close"
    };

    private static Dictionary<QuarterSpecialty, string> _descriptions = new Dictionary<QuarterSpecialty, string>
    {
        { QuarterSpecialty.Market, "A bustling trade district. Gold income from all sources increased." },
        { QuarterSpecialty.Industrial, "Forges and workshops line the streets. Blacksmith and physical goal bonuses improved." },
        { QuarterSpecialty.Scholar, "Ink-stained scholars crowd the lanes. Library bonuses and educational goal rewards improved." },
        { QuarterSpecialty.Clerical, "Quiet chapels and herb gardens. Spiritual goal rewards and cleric contributions improved." },
        { QuarterSpecialty.Military, "Barracks and training yards. Village combat power increased." },
        { QuarterSpecialty.Artisan, "Craftspeople and performers share close quarters. Social goal rewards and bard contributions improved." }
    };

    public Quarter(string name, QuarterSpecialty specialty, int bonusValue)
    {
        _name = name;
        _specialty = specialty;
        _bonusValue = bonusValue;
        _description = _descriptions[specialty];
    }

    public string GetName() { return _name; }
    public QuarterSpecialty GetSpecialty() { return _specialty; }
    public int GetBonusValue() { return _bonusValue; }

    public static Quarter Generate(int expansionTier)
    {
        Random rng = new Random(DateTime.Now.Millisecond + expansionTier * 37);
        string prefix = _prefixes[rng.Next(_prefixes.Count)];
        string suffix = _suffixes[rng.Next(_suffixes.Count)];
        string name = prefix + " " + suffix;

        Array specialties = Enum.GetValues(typeof(QuarterSpecialty));
        QuarterSpecialty specialty = (QuarterSpecialty)specialties.GetValue(rng.Next(specialties.Length));

        int bonus = 10 + (expansionTier * 5);
        return new Quarter(name, specialty, bonus);
    }

    public string GetStatusString()
    {
        return _name + " [" + _specialty + "]\n  " + _description + "\n  Bonus: +" + _bonusValue + "%";
    }

    public string Encode()
    {
        return _name + "|" + _specialty + "|" + _bonusValue;
    }
}
