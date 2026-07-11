using System;
using System.Collections.Generic;
using System.IO;

namespace ScrollsAndSteel
{
    // Handles saving a Character to a text file and loading one back.
    // Follows the same pattern as the FileManager used in the Gamification
    // project: the object being saved encodes itself into a line of text,
    // and this class writes/reads that line and decodes it back into an object.
    public class CharacterFileManager
    {
        public void SaveCharacter(Character character, string filename)
        {
            string line = character.Encode();
            File.WriteAllText(filename, line);
            Console.WriteLine("Character saved to " + filename);
        }

        public Character LoadCharacter(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found: " + filename);
                return null;
            }

            string line = File.ReadAllText(filename);
            return DecodeCharacter(line);
        }

        // Turns one saved line of text back into a Character. The line looks
        // like: Name|RaceName|STR:65,END:55,...|Long Blade:40,Sneak:25,...
        private Character DecodeCharacter(string line)
        {
            string[] mainParts = line.Split('|');
            string name = mainParts[0];
            string raceName = mainParts[1];
            string attributeSection = mainParts[2];
            string skillSection = mainParts[3];

            Race race = RaceFactory.CreateByName(raceName);

            Dictionary<string, int> attributeValues = new Dictionary<string, int>();
            string[] attributeEntries = attributeSection.Split(',');
            foreach (string entry in attributeEntries)
            {
                string[] pair = entry.Split(':');
                string attributeName = pair[0];
                int attributeValue = int.Parse(pair[1]);
                attributeValues[attributeName] = attributeValue;
            }

            // false = do not re-apply racial bonuses, since these values are
            // already final.
            Character character = new Character(name, race, attributeValues, false);

            string[] skillEntries = skillSection.Split(',');
            foreach (string entry in skillEntries)
            {
                string[] pair = entry.Split(':');
                string skillName = pair[0];
                int skillValue = int.Parse(pair[1]);

                string[] governingNames = SkillDefinitions.All[skillName];
                Attribute[] governingAttributes = new Attribute[governingNames.Length];
                for (int i = 0; i < governingNames.Length; i++)
                {
                    governingAttributes[i] = character.GetAttribute(governingNames[i]);
                }

                character.AddSkill(new Skill(skillName, skillValue, governingAttributes));
            }

            character.CalculateDerivedStats();
            return character;
        }
    }
}
