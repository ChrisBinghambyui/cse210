using System;
using System.Collections.Generic;

enum QuarterSpecialty { Market, Industrial, Scholar, Clerical, Military, Artisan }

class Quarter
{
    private string _name;
    private QuarterSpecialty _specialty;
    private int _bonusValue;

    private static string[] _prefixes =
    {
        "Ashfen", "Ironveil", "Goldmere", "Duskhollow", "Embervane", "Coldwater",
        "Thornwick", "Salthaven", "Coppergate", "Bramble", "Nightfall", "Stonebridge",
        "Silverbrook", "Rustholm", "Cindergate", "Mistwood", "Ironbell", "Greyveil"
    };

    private static string[] _suffixes =
    {
        "Row", "Quarter", "Ward", "District", "Cross", "Lane", "Gate",
        "Hollow", "Rise", "Reach", "Passage", "Common", "Green", "Close"
    };

    private static Dictionary<QuarterSpecialty, string> _descriptions = new Dictionary<QuarterSpecialty, string>
    {
        { QuarterSpecialty.Market,    "A bustling trade district. Gold income from all sources increased." },
        { QuarterSpecialty.Industrial,"Forges and workshops line the streets. Blacksmith and physical goal bonuses improved." },
        { QuarterSpecialty.Scholar,   "Ink-stained scholars crowd the lanes. Library bonuses and educational goal rewards improved." },
        { QuarterSpecialty.Clerical,  "Quiet chapels and herb gardens. Spiritual goal rewards and cleric contributions improved." },
        { QuarterSpecialty.Military,  "Barracks and training yards. Village combat power increased." },
        { QuarterSpecialty.Artisan,   "Craftspeople and performers share close quarters. Social goal rewards and bard contributions improved." }
    };

    public Quarter(string name, QuarterSpecialty specialty, int bonusValue)
    {
        _name = name;
        _specialty = specialty;
        _bonusValue = bonusValue;
    }

    public string GetName() => _name;
    public QuarterSpecialty GetSpecialty() => _specialty;
    public int GetBonusValue() => _bonusValue;

    public static Quarter Generate(int expansionTier)
    {
        Random rng = new Random(DateTime.Now.Millisecond + expansionTier * 37);
        string name = $"{_prefixes[rng.Next(_prefixes.Length)]} {_suffixes[rng.Next(_suffixes.Length)]}";
        QuarterSpecialty specialty = (QuarterSpecialty)rng.Next(Enum.GetValues(typeof(QuarterSpecialty)).Length);
        return new Quarter(name, specialty, 10 + (expansionTier * 5));
    }

    public string GetStatusString() =>
        $"{_name} [{_specialty}]\n  {_descriptions[_specialty]}\n  Bonus: +{_bonusValue}%";

    public string Encode() => $"{_name}|{_specialty}|{_bonusValue}";
}
