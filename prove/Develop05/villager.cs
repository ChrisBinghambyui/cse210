using System;
using System.Collections.Generic;

enum VillagerSpecialty { Blacksmith, Scholar, Merchant, Cleric, Ranger, Bard }

class Villager
{
    private string _firstName;
    private string _lastName;
    private VillagerSpecialty _specialty;
    private int _tier;
    private DateTime _arrivalDate;
    private bool _hasOfferedQuest;
    private bool _questComplete;
    private Commission _personalQuest;

    private static string[] _firstNames =
    {
        "Aldric", "Maren", "Torben", "Selja", "Finn", "Bryn", "Oswin", "Lirien",
        "Cael", "Mira", "Hadwin", "Thyra", "Rowan", "Edda", "Gareth", "Seren",
        "Wulf", "Anya", "Bram", "Isolde", "Dag", "Nora", "Leif", "Astrid"
    };

    private static string[] _lastNames =
    {
        "Ashveil", "Ironmoor", "Coldwater", "Thornwick", "Embervane", "Stonefall",
        "Duskholm", "Greywood", "Saltfen", "Coppergate", "Bramblefield", "Nighthollow"
    };

    private static Dictionary<VillagerSpecialty, string[]> _titles = new Dictionary<VillagerSpecialty, string[]>
    {
        { VillagerSpecialty.Blacksmith, new[] { "Apprentice Smith", "Adept Forgeworker", "Master Armorer" } },
        { VillagerSpecialty.Scholar,    new[] { "Apprentice Scholar", "Learned Scribe", "Arcane Researcher" } },
        { VillagerSpecialty.Merchant,   new[] { "Market Stall Keeper", "Established Trader", "Guild Merchant" } },
        { VillagerSpecialty.Cleric,     new[] { "Acolyte", "Village Chaplain", "High Cleric" } },
        { VillagerSpecialty.Ranger,     new[] { "Wandering Scout", "Seasoned Ranger", "Warden of the Wilds" } },
        { VillagerSpecialty.Bard,       new[] { "Traveling Minstrel", "Court Performer", "Legendary Bard" } }
    };

    private static Dictionary<VillagerSpecialty, string[]> _personalQuestPool = new Dictionary<VillagerSpecialty, string[]>
    {
        { VillagerSpecialty.Blacksmith, new[]
            {
                "Complete 25 push-ups in a single session|Prove your strength in one go|Physical|200|100",
                "Work out every day for 2 weeks|Build a habit of daily exercise|Physical|300|150",
                "Go on 10 walks of at least 30 minutes each|Build endurance through walking|Physical|250|125"
            }
        },
        { VillagerSpecialty.Scholar, new[]
            {
                "Study for at least 60 minutes a day for 2 weeks straight|Commit to deep focused learning|Educational|400|200",
                "Read an entire book of scripture from the Book of Mormon|Complete a full book in study|Educational|350|175",
                "Write detailed notes on 5 different topics you want to master|Turn curiosity into knowledge|Educational|300|150"
            }
        },
        { VillagerSpecialty.Merchant, new[]
            {
                "Track every purchase you make for a full month|Build financial awareness|Educational|300|150",
                "Go one full week without any unnecessary spending|Practice financial discipline|Educational|250|125",
                "Cook every meal at home for 2 weeks|Save money and build a skill|Physical|280|140"
            }
        },
        { VillagerSpecialty.Cleric, new[]
            {
                "Listen to a full session of General Conference|Sit with the words of modern prophets|Spiritual|350|175",
                "Read the entire book of Mosiah|Immerse yourself in scripture|Spiritual|400|200",
                "Pray morning and night every day for 30 days|Build a consistent prayer habit|Spiritual|350|175"
            }
        },
        { VillagerSpecialty.Ranger, new[]
            {
                "Go on 5 hikes of at least 1 hour each|Explore and build physical stamina|Physical|300|150",
                "Spend time in nature for at least 30 minutes every day for a week|Ground yourself outside|Spiritual|250|125",
                "Run a total of 15 miles across any number of sessions|Build running endurance|Physical|350|175"
            }
        },
        { VillagerSpecialty.Bard, new[]
            {
                "Write in your journal every day for 3 weeks|Build a reflective writing habit|Spiritual|320|160",
                "Have a meaningful conversation with 5 different people|Practice deep connection|Social|280|140",
                "Perform an act of service for a stranger 3 times|Let your kindness reach beyond your circle|Social|300|150"
            }
        }
    };

    public Villager(string firstName, string lastName, VillagerSpecialty specialty)
        : this(firstName, lastName, specialty, 0, DateTime.Now, false, false) { }

    public Villager(string firstName, string lastName, VillagerSpecialty specialty,
        int tier, DateTime arrivalDate, bool hasOfferedQuest, bool questComplete)
    {
        _firstName = firstName;
        _lastName = lastName;
        _specialty = specialty;
        _tier = tier;
        _arrivalDate = arrivalDate;
        _hasOfferedQuest = hasOfferedQuest;
        _questComplete = questComplete;
    }

    public static Villager GenerateRandom()
    {
        Random rng = new Random();
        return new Villager(
            _firstNames[rng.Next(_firstNames.Length)],
            _lastNames[rng.Next(_lastNames.Length)],
            (VillagerSpecialty)rng.Next(Enum.GetValues(typeof(VillagerSpecialty)).Length)
        );
    }

    public string GetFullName() => $"{_firstName} {_lastName}";
    public VillagerSpecialty GetSpecialty() => _specialty;
    public int GetTier() => _tier;
    public bool HasOfferedQuest() => _hasOfferedQuest;
    public bool IsQuestComplete() => _questComplete;
    public Commission GetPersonalQuest() => _personalQuest;
    public DateTime GetVillagerArrivalDate() => _arrivalDate;
    public float GetContributionMultiplier() => 1.0f + (_tier * 0.25f);
    public bool CanOfferQuest() => !_hasOfferedQuest && (DateTime.Now - _arrivalDate).TotalDays >= 7;

    public string GetTitle()
    {
        int index = Math.Min(_tier, _titles[_specialty].Length - 1);
        return _titles[_specialty][index];
    }

    public Commission GeneratePersonalQuest()
    {
        if (_personalQuest != null) return _personalQuest;
        string[] pool = _personalQuestPool[_specialty];
        string[] parts = pool[new Random().Next(pool.Length)].Split("|");
        GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[2]);
        _personalQuest = new Commission(parts[0], parts[1], int.Parse(parts[3]), int.Parse(parts[4]),
            type, CommissionFrequency.Weekly, DateTime.MaxValue, GetFullName());
        _hasOfferedQuest = true;
        return _personalQuest;
    }

    public void CompleteQuest()
    {
        _questComplete = true;
        _tier = Math.Min(_tier + 1, 2);
    }

    public string GetStatusString()
    {
        string s = $"{GetFullName()}, {GetTitle()} ({_specialty})" +
                   $"\n  Arrived: {_arrivalDate:MMM dd yyyy}" +
                   $"\n  Contribution bonus: x{GetContributionMultiplier():0.00}";
        if (CanOfferQuest()) s += "\n  [Has a personal quest to offer!]";
        return s;
    }

    public string Encode() =>
        $"{_firstName}|{_lastName}|{_specialty}|{_tier}|{_arrivalDate.Ticks}|{_hasOfferedQuest}|{_questComplete}";
}
