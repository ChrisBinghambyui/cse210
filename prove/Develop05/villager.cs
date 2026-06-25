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

    private static List<string> _firstNames = new List<string>
    {
        "Aldric", "Maren", "Torben", "Selja", "Finn", "Bryn", "Oswin", "Lirien",
        "Cael", "Mira", "Hadwin", "Thyra", "Rowan", "Edda", "Gareth", "Seren",
        "Wulf", "Anya", "Bram", "Isolde", "Dag", "Nora", "Leif", "Astrid"
    };

    private static List<string> _lastNames = new List<string>
    {
        "Ashveil", "Ironmoor", "Coldwater", "Thornwick", "Embervane", "Stonefall",
        "Duskholm", "Greywood", "Saltfen", "Coppergate", "Bramblefield", "Nighthollow"
    };

    private static Dictionary<VillagerSpecialty, string[]> _titles = new Dictionary<VillagerSpecialty, string[]>
    {
        { VillagerSpecialty.Blacksmith, new string[] { "Apprentice Smith", "Adept Forgeworker", "Master Armorer" } },
        { VillagerSpecialty.Scholar, new string[] { "Apprentice Scholar", "Learned Scribe", "Arcane Researcher" } },
        { VillagerSpecialty.Merchant, new string[] { "Market Stall Keeper", "Established Trader", "Guild Merchant" } },
        { VillagerSpecialty.Cleric, new string[] { "Acolyte", "Village Chaplain", "High Cleric" } },
        { VillagerSpecialty.Ranger, new string[] { "Wandering Scout", "Seasoned Ranger", "Warden of the Wilds" } },
        { VillagerSpecialty.Bard, new string[] { "Traveling Minstrel", "Court Performer", "Legendary Bard" } }
    };

    private static Dictionary<VillagerSpecialty, List<string>> _personalQuestPool = new Dictionary<VillagerSpecialty, List<string>>
    {
        { VillagerSpecialty.Blacksmith, new List<string>
            {
                "Complete 25 push-ups in a single session|Prove your strength in one go|Physical|200|100",
                "Work out every day for 2 weeks|Build a habit of daily exercise|Physical|300|150",
                "Go on 10 walks of at least 30 minutes each|Build endurance through walking|Physical|250|125"
            }
        },
        { VillagerSpecialty.Scholar, new List<string>
            {
                "Study for at least 60 minutes a day for 2 weeks straight|Commit to deep focused learning|Educational|400|200",
                "Read an entire book of scripture from the Book of Mormon|Complete a full book in study|Educational|350|175",
                "Write detailed notes on 5 different topics you want to master|Turn curiosity into knowledge|Educational|300|150"
            }
        },
        { VillagerSpecialty.Merchant, new List<string>
            {
                "Track every purchase you make for a full month|Build financial awareness|Educational|300|150",
                "Go one full week without any unnecessary spending|Practice financial discipline|Educational|250|125",
                "Cook every meal at home for 2 weeks|Save money and build a skill|Physical|280|140"
            }
        },
        { VillagerSpecialty.Cleric, new List<string>
            {
                "Listen to a full session of General Conference|Sit with the words of modern prophets|Spiritual|350|175",
                "Read the entire book of Mosiah|Immerse yourself in scripture|Spiritual|400|200",
                "Pray morning and night every day for 30 days|Build a consistent prayer habit|Spiritual|350|175"
            }
        },
        { VillagerSpecialty.Ranger, new List<string>
            {
                "Go on 5 hikes of at least 1 hour each|Explore and build physical stamina|Physical|300|150",
                "Spend time in nature for at least 30 minutes every day for a week|Ground yourself outside|Spiritual|250|125",
                "Run a total of 15 miles across any number of sessions|Build running endurance|Physical|350|175"
            }
        },
        { VillagerSpecialty.Bard, new List<string>
            {
                "Write in your journal every day for 3 weeks|Build a reflective writing habit|Spiritual|320|160",
                "Have a meaningful conversation with 5 different people|Practice deep connection|Social|280|140",
                "Perform an act of service for a stranger 3 times|Let your kindness reach beyond your circle|Social|300|150"
            }
        }
    };

    public Villager(string firstName, string lastName, VillagerSpecialty specialty)
    {
        _firstName = firstName;
        _lastName = lastName;
        _specialty = specialty;
        _tier = 0;
        _arrivalDate = DateTime.Now;
        _hasOfferedQuest = false;
        _questComplete = false;
        _personalQuest = null;
    }

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
        _personalQuest = null;
    }

    public static Villager GenerateRandom()
    {
        Random rng = new Random();
        string first = _firstNames[rng.Next(_firstNames.Count)];
        string last = _lastNames[rng.Next(_lastNames.Count)];
        VillagerSpecialty specialty = (VillagerSpecialty)rng.Next(Enum.GetValues(typeof(VillagerSpecialty)).Length);
        return new Villager(first, last, specialty);
    }

    public string GetFullName() { return _firstName + " " + _lastName; }
    public VillagerSpecialty GetSpecialty() { return _specialty; }
    public int GetTier() { return _tier; }
    public bool HasOfferedQuest() { return _hasOfferedQuest; }
    public bool IsQuestComplete() { return _questComplete; }
    public Commission GetPersonalQuest() { return _personalQuest; }

    public string GetTitle()
    {
        int titleIndex = Math.Min(_tier, _titles[_specialty].Length - 1);
        return _titles[_specialty][titleIndex];
    }

    public float GetContributionMultiplier()
    {
        return 1.0f + (_tier * 0.25f);
    }

    public bool CanOfferQuest()
    {
        return !_hasOfferedQuest && (DateTime.Now - _arrivalDate).TotalDays >= 7;
    }

    public Commission GeneratePersonalQuest()
    {
        if (_personalQuest != null) return _personalQuest;

        List<string> pool = _personalQuestPool[_specialty];
        Random rng = new Random();
        string[] parts = pool[rng.Next(pool.Count)].Split("|");
        GoalType type = (GoalType)Enum.Parse(typeof(GoalType), parts[2]);

        _personalQuest = new Commission(
            parts[0], parts[1], int.Parse(parts[3]), int.Parse(parts[4]),
            type, CommissionFrequency.Weekly, DateTime.MaxValue, GetFullName()
        );
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
        string status = GetFullName() + ", " + GetTitle() + " (" + _specialty + ")";
        status += "\n  Arrived: " + _arrivalDate.ToString("MMM dd yyyy");
        status += "\n  Contribution bonus: x" + GetContributionMultiplier().ToString("0.00");
        if (CanOfferQuest())
        {
            status += "\n  [Has a personal quest to offer!]";
        }
        return status;
    }

    public DateTime GetVillagerArrivalDate() { return _arrivalDate; }

    public string Encode()
    {
        return _firstName + "|" + _lastName + "|" + _specialty + "|" + _tier + "|"
            + _arrivalDate.Ticks + "|" + _hasOfferedQuest + "|" + _questComplete;
    }
}

// extension needed by VillagerManager
// added as a partial method block
