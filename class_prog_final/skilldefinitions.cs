using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public static class SkillDefinitions
    {
        public static readonly Dictionary<string, string[]> All = new Dictionary<string, string[]>
        {
            { "Long Blade",    new[] { "STR", "AGI" } },
            { "Short Blade",   new[] { "AGI" } },
            { "Blunt Weapon",  new[] { "STR" } },
            { "Axe",           new[] { "STR" } },
            { "Spear",         new[] { "STR", "AGI" } },
            { "Marksman",      new[] { "AGI" } },
            { "Block",         new[] { "STR", "AGI" } },
            { "Unarmored",     new[] { "AGI" } },
            { "Light Armor",   new[] { "AGI" } },
            { "Medium Armor",  new[] { "END" } },
            { "Heavy Armor",   new[] { "END", "STR" } },
            { "Athletics",     new[] { "SPD", "END" } },
            { "Acrobatics",    new[] { "AGI" } },
            { "Sneak",         new[] { "AGI" } },
            { "Security",      new[] { "AGI", "INT" } },
            { "Speechcraft",   new[] { "PER" } },
            { "Mercantile",    new[] { "PER", "INT" } },
            { "Alchemy",       new[] { "INT" } },
            { "Enchanting",    new[] { "INT", "WIL" } },
            { "Conjuration",   new[] { "INT", "WIL" } },
            { "Alteration",    new[] { "INT", "WIL" } },
            { "Elementalism",  new[] { "INT", "WIL" } },
            { "Illusion",      new[] { "INT", "PER" } },
            { "Divination",    new[] { "WIL", "INT" } },
            { "Vitalism",      new[] { "WIL" } },
            { "Armorer",       new[] { "STR", "INT" } },
            { "Fistfight",     new[] { "STR", "AGI" } },
        };
    }
}
