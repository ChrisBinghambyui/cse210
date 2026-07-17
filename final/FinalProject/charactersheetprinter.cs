using System;
using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class CharacterSheetPrinter
    {
        private static readonly string[] AttributeOrder =
            { "STR", "END", "AGI", "SPD", "INT", "WIL", "PER", "LCK" };

        // "Agility Damage Bonus" is the longest label in the Derived Statistics block.
        private const int DerivedLabelWidth = 21;

        public void DisplaySheet(Character character)
        {
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine(" " + character.GetName() + "  -  Level " + character.GetLevel() +
                " " + character.GetRace().GetName());
            Console.WriteLine("==================================================");

            Console.WriteLine();
            Console.WriteLine("Attributes:");
            Console.Write(FormatAttributes(character));

            Console.WriteLine();
            Console.WriteLine("Derived Statistics:");
            Console.Write(FormatDerivedStats(character));

            Console.WriteLine();
            Console.WriteLine("Skills:");
            Console.Write(FormatSkills(character));
            Console.WriteLine();
        }

        private string FormatDerivedStats(Character character)
        {
            string result = "";
            result = result + FormatDerivedLine("HP", character.GetCurrentHP() + "/" + character.GetMaxHP());
            result = result + FormatDerivedLine("MP", character.GetCurrentMP() + "/" + character.GetMaxMP());
            result = result + FormatDerivedLine("FP", character.GetCurrentFP() + "/" + character.GetMaxFP());
            result = result + FormatDerivedLine("Initiative Bonus", "+" + character.GetInitiativeBonus());
            result = result + FormatDerivedLine("Luck Bonus", "+" + character.GetLuckBonus());
            result = result + FormatDerivedLine("Melee Damage Bonus", "+" + character.GetMeleeDamageBonus());
            result = result + FormatDerivedLine("Agility Damage Bonus", "+" + character.GetAgilityDamageBonus());
            result = result + FormatDerivedLine("Movement Speed", character.GetMovementSpeed() + "m per turn");
            result = result + FormatDerivedLine("Carry Weight", character.GetCarryWeight() + "kg");
            return result;
        }

        private string FormatDerivedLine(string label, string value)
        {
            return "  " + label.PadRight(DerivedLabelWidth) + ": " + value + "\n";
        }

        private string FormatAttributes(Character character)
        {
            string result = "";
            for (int i = 0; i < AttributeOrder.Length; i++)
            {
                string name = AttributeOrder[i];
                int value = character.GetAttribute(name).GetValue();
                result = result + "  " + name.PadRight(4) + ": " + value + "\n";
            }
            return result;
        }

        private string FormatSkills(Character character)
        {
            string result = "";
            List<Skill> skills = character.GetAllSkills();
            for (int i = 0; i < skills.Count; i++)
            {
                Skill skill = skills[i];
                result = result + "  " + skill.GetName().PadRight(14) + ": " +
                    skill.GetValue() + " (cap " + skill.GetCap() + ")\n";
            }
            return result;
        }
    }
}
