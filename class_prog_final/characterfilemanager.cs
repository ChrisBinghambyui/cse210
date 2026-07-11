using System;
using System.Collections.Generic;
using System.IO;

namespace ScrollsAndSteel
{
    public class CharacterFileManager
    {
        private const string FolderName = "character sheets";

        public bool SaveCharacter(Character character, string desiredName)
        {
            Directory.CreateDirectory(FolderName);

            string fullPath = Path.Combine(FolderName, desiredName + ".txt");

            if (File.Exists(fullPath))
            {
                Console.WriteLine("A character sheet named '" + desiredName + "' already exists. Choose a different name.");
                return false;
            }

            string line = character.Encode();
            File.WriteAllText(fullPath, line);
            Console.WriteLine("Character saved as '" + desiredName + "'.");
            return true;
        }

        public List<string> ListSavedCharacterNames()
        {
            Directory.CreateDirectory(FolderName);

            List<string> names = new List<string>();
            string[] filePaths = Directory.GetFiles(FolderName, "*.txt");
            for (int i = 0; i < filePaths.Length; i++)
            {
                string nameOnly = Path.GetFileNameWithoutExtension(filePaths[i]);
                names.Add(nameOnly);
            }
            return names;
        }

        public Character LoadCharacter(string characterName)
        {
            string fullPath = Path.Combine(FolderName, characterName + ".txt");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File not found: " + fullPath);
                return null;
            }

            string line = File.ReadAllText(fullPath);
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
