using System;
using System.Collections.Generic;
using System.IO;

namespace ScrollsAndSteel
{
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
