using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public static class SkillDefinitions
    {
        public static readonly Dictionary<string, string[]> All = new Dictionary<string, string[]>
        {
            { "Long Blade",    new string[] { "STR", "AGI" } },
            { "Short Blade",   new string[] { "AGI" } },
            { "Blunt Weapon",  new string[] { "STR" } },
            { "Axe",           new string[] { "STR" } },
            { "Spear",         new string[] { "STR", "AGI" } },
            { "Marksman",      new string[] { "AGI" } },
            { "Block",         new string[] { "STR", "AGI" } },
            { "Unarmored",     new string[] { "AGI" } },
            { "Light Armor",   new string[] { "AGI" } },
            { "Medium Armor",  new string[] { "END" } },
            { "Heavy Armor",   new string[] { "END", "STR" } },
            { "Athletics",     new string[] { "SPD", "END" } },
            { "Acrobatics",    new string[] { "AGI" } },
            { "Sneak",         new string[] { "AGI" } },
            { "Security",      new string[] { "AGI", "INT" } },
            { "Speechcraft",   new string[] { "PER" } },
            { "Mercantile",    new string[] { "PER", "INT" } },
            { "Alchemy",       new string[] { "INT" } },
            { "Enchanting",    new string[] { "INT", "WIL" } },
            { "Conjuration",   new string[] { "INT", "WIL" } },
            { "Alteration",    new string[] { "INT", "WIL" } },
            { "Elementalism",  new string[] { "INT", "WIL" } },
            { "Illusion",      new string[] { "INT", "PER" } },
            { "Divination",    new string[] { "WIL", "INT" } },
            { "Vitalism",      new string[] { "WIL" } },
            { "Armorer",       new string[] { "STR", "INT" } },
            { "Fistfight",     new string[] { "STR", "AGI" } },
        };

        public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
        {
            { "Long Blade",    "Swords, sabers, and longswords." },
            { "Short Blade",   "Daggers, dirks, and short swords." },
            { "Blunt Weapon",  "Maces, clubs, warhammers, and staves used as bludgeons." },
            { "Axe",           "Hand axes, war axes, and greataxes." },
            { "Spear",         "Polearms and thrusting weapons. Extended reach." },
            { "Marksman",      "Bows, crossbows, and thrown weapons." },
            { "Block",         "Active defense with a shield or two-handed weapon parry." },
            { "Unarmored",     "Defense and fluid movement without armor." },
            { "Light Armor",   "Leather, hide, and padded armor." },
            { "Medium Armor",  "Chainmail, scale, and brigandine." },
            { "Heavy Armor",   "Plate and full steel." },
            { "Athletics",     "Running, swimming, climbing, and sustained movement." },
            { "Acrobatics",    "Tumbling, vaulting, and falling safely." },
            { "Sneak",         "Moving unseen and unheard." },
            { "Security",      "Picking locks and disarming traps." },
            { "Speechcraft",   "Persuasion, deception, and intimidation." },
            { "Mercantile",    "Buying, selling, and negotiating to your advantage." },
            { "Alchemy",       "Brewing potions and poisons from raw ingredients." },
            { "Enchanting",    "Binding magical effects permanently into items." },
            { "Conjuration",   "Summoning creatures and binding spirits from other planes." },
            { "Alteration",    "Bending physical laws: weight, water, locks, and more." },
            { "Elementalism",  "Offensive elemental magic: fire, frost, and shock." },
            { "Illusion",      "Manipulating perception and the senses." },
            { "Divination",    "Soul-work, telekinesis, detection, and spell-weaving." },
            { "Vitalism",      "Healing wounds and curing ailments." },
            { "Armorer",       "Repairing and maintaining weapons and armor. Also used to craft basic equipment at a forge." },
            { "Fistfight",     "Attacks made while unarmed or with fist wraps and gauntlets." },
        };

        public static readonly string[] WarriorSkills = new string[]
        {
            "Long Blade", "Blunt Weapon", "Axe", "Spear", "Block",
            "Heavy Armor", "Medium Armor", "Athletics", "Fistfight"
        };

        public static readonly string[] MageSkills = new string[]
        {
            "Alchemy", "Enchanting", "Conjuration", "Alteration", "Elementalism",
            "Illusion", "Divination", "Vitalism", "Armorer"
        };

        public static readonly string[] ThiefSkills = new string[]
        {
            "Short Blade", "Marksman", "Unarmored", "Light Armor", "Acrobatics",
            "Sneak", "Security", "Speechcraft", "Mercantile"
        };

        public static readonly string[] MagicSchools = new string[]
        {
            "Alchemy", "Enchanting", "Conjuration", "Alteration",
            "Elementalism", "Illusion", "Divination", "Vitalism"
        };

        public static readonly string[] NonCombatSkills = new string[]
        {
            "Athletics", "Acrobatics", "Sneak", "Security", "Speechcraft", "Mercantile",
            "Alchemy", "Enchanting", "Conjuration", "Alteration",
            "Elementalism", "Illusion", "Divination", "Vitalism", "Armorer"
        };
    }
}
