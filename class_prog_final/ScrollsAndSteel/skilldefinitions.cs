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
    }
}
