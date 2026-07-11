using System;
using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class CharacterSheetPrinter
    {
        private static readonly string[] AttributeOrder =
            { "STR", "END", "AGI", "SPD", "INT", "WIL", "PER", "LCK" };

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
            Console.WriteLine("  HP: " + character.GetCurrentHP() + "/" + character.GetMaxHP());
            Console.WriteLine("  MP: " + character.GetCurrentMP() + "/" + character.GetMaxMP());
            Console.WriteLine("  FP: " + character.GetCurrentFP() + "/" + character.GetMaxFP());
            Console.WriteLine("  Initiative Bonus: +" + character.GetInitiativeBonus());
            Console.WriteLine("  Luck Bonus: +" + character.GetLuckBonus());
            Console.WriteLine("  Melee Damage Bonus: +" + character.GetMeleeDamageBonus());
            Console.WriteLine("  Agility Damage Bonus: +" + character.GetAgilityDamageBonus());
            Console.WriteLine("  Movement Speed: " + character.GetMovementSpeed() + "m per turn");
            Console.WriteLine("  Carry Weight: " + character.GetCarryWeight() + "kg");

            Console.WriteLine();
            Console.WriteLine("Skills:");
            Console.Write(FormatSkills(character));
            Console.WriteLine();
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
