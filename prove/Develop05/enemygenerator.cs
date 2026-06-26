using System;

class EnemyGenerator
{
    private static string[] _descriptors =
    {
        "Towering", "Puny", "Massive", "Withered", "Hulking", "Gaunt", "Bloated", "Grey", "Shimmering", "Obsidian", "Crimson", "Pale", "Ashen", "Gilded", "Verdant", "Ivory", "Murky", "Violet", "Ember-touched", "Menacing", "Feral", "Emaciated", "Frenzied", "Sullen", "Ravenous", "Cunning", "Wretched", "Hollow-eyed", "Maddened", "Vengeful", "Lurking", "Undead", "Cursed", "Ancient", "Rotting", "Spectral", "Iron", "Plague", "Stone", "Frostbite", "Swamp", "Sand", "Ash", "Bog"
    };

    private static string[] _creatureTypes =
    {
        "Bandit", "Rogue", "Minotaur", "Rat Swarm", "Wraith", "Goblin", "Troll", "Skeleton", "Orc", "Harpy", "Slime", "Wyvern", "Cultist", "Specter", "Spider", "Golem", "Kobold", "Werewolf", "Basilisk", "Ghoul", "Witch", "Hound", "Serpent", "Revenant", "Gargoyle", "Drake"
    };

    public static Enemy Generate(int playerLevel)
    {
        Random rng = new Random(DateTime.Now.DayOfYear + DateTime.Now.Year);

        string name = $"{_descriptors[rng.Next(_descriptors.Length)]} {_creatureTypes[rng.Next(_creatureTypes.Length)]}";
        int health = 20 + (playerLevel * 10) + rng.Next(-5, 15);

        Array types = Enum.GetValues(typeof(GoalType));
        GoalType requiredType = (GoalType)types.GetValue(rng.Next(types.Length - 1));

        return new Enemy(
            name,
            health,
            requiredType,
            goalsRequired: rng.Next(1, 4),
            goldReward: 30 + (playerLevel * 10) + rng.Next(0, 20),
            xpReward: 50 + (playerLevel * 15) + rng.Next(0, 25)
        );
    }
}