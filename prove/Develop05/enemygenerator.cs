using System;
using System.Collections.Generic;

class EnemyGenerator
{
    private static List<string> _sizePrefixes = new List<string>
    {
        "Towering", "Puny", "Massive", "Withered", "Hulking", "Gaunt", "Bloated", "Tiny"
    };

    private static List<string> _colorPrefixes = new List<string>
    {
        "Grey", "Shimmering", "Obsidian", "Crimson", "Pale", "Ashen", "Gilded",
        "Verdant", "Ivory", "Murky", "Violet", "Ember-touched"
    };

    private static List<string> _moodPrefixes = new List<string>
    {
        "Menacing", "Feral", "Emaciated", "Frenzied", "Sullen", "Ravenous",
        "Cunning", "Wretched", "Hollow-eyed", "Maddened", "Vengeful", "Lurking"
    };

    private static List<string> _creatureTypes = new List<string>
    {
        "Bandit", "Rogue", "Minotaur", "Rat", "Wraith", "Goblin", "Troll",
        "Skeleton", "Orc", "Harpy", "Slime", "Wyvern", "Cultist", "Specter",
        "Giant Spider", "Golem", "Kobold", "Werewolf", "Basilisk", "Ghoul",
        "Undead Knight", "Bog Witch", "Frostbite Hound", "Iron Serpent",
        "Sand Revenant", "Plague Rat", "Stone Gargoyle", "Swamp Lurker",
        "Cursed Merchant", "Fallen Paladin", "Ash Drake", "Cave Fisher"
    };

    public static Enemy Generate(int playerLevel)
    {
        Random rng = new Random(DateTime.Now.DayOfYear + DateTime.Now.Year);

        string size = _sizePrefixes[rng.Next(_sizePrefixes.Count)];
        string color = _colorPrefixes[rng.Next(_colorPrefixes.Count)];
        string mood = _moodPrefixes[rng.Next(_moodPrefixes.Count)];
        string creature = _creatureTypes[rng.Next(_creatureTypes.Count)];
        string name = mood + " " + color + " " + size + " " + creature;

        int baseHealth = 20 + (playerLevel * 10);
        int health = baseHealth + rng.Next(-5, 15);

        Array types = Enum.GetValues(typeof(GoalType));
        GoalType requiredType = (GoalType)types.GetValue(rng.Next(types.Length - 1));

        int goalsRequired = rng.Next(1, 4);
        int resistance = playerLevel * 2;
        int goldReward = 30 + (playerLevel * 10) + rng.Next(0, 20);
        int xpReward = 50 + (playerLevel * 15) + rng.Next(0, 25);

        return new Enemy(name, health, requiredType, goalsRequired, resistance, goldReward, xpReward);
    }
}
