using System;
using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class Program
    {
        private int _menuChoice;
        private Character _currentCharacter;
        private CharacterSheetPrinter _printer;
        private CharacterFileManager _fileManager;

        private static readonly string[] AttributeNames =
            { "STR", "END", "AGI", "SPD", "INT", "WIL", "PER", "LCK" };

        private static readonly string[] RaceNames =
        {
            "Solarirum", "Sylvarirum", "Vethanirum", "Ashirum",
            "Apisdrenn", "Apiskeld", "Apisveldir", "Kreln",
            "Imperials", "Nordal", "Sunblade", "Ashveld",
            "Stoneguard", "Gorirum", "Veildrift", "Murrak",
            "Bovari", "Naukin", "Arantza", "Verdathi"
        };

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public Program()
        {
            _printer = new CharacterSheetPrinter();
            _fileManager = new CharacterFileManager();
        }

        private void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                _menuChoice = Menu();

                if (_menuChoice == 1)
                {
                    _currentCharacter = CreateCharacter();
                }
                else if (_menuChoice == 2)
                {
                    DisplayCharacterSheet(_currentCharacter);
                }
                else if (_menuChoice == 3)
                {
                    RunSkillCheckDemo(_currentCharacter);
                }
                else if (_menuChoice == 4)
                {
                    SaveCharacterToFile(_currentCharacter);
                }
                else if (_menuChoice == 5)
                {
                    Character loadedCharacter = LoadCharacterFromFile();
                    if (loadedCharacter != null)
                    {
                        _currentCharacter = loadedCharacter;
                    }
                }
                else if (_menuChoice == 6)
                {
                    keepRunning = false;
                }
            }
            Console.WriteLine("Farewell, traveler.");
        }

        public int Menu()
        {
            Console.WriteLine();
            Console.WriteLine("=== Scrolls & Steel Character Builder ===");
            Console.WriteLine("1. Create a new character");
            Console.WriteLine("2. View character sheet");
            Console.WriteLine("3. Run a skill check");
            Console.WriteLine("4. Save character to file");
            Console.WriteLine("5. Load character from file");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");
            return ReadIntInRange(1, 6);
        }

        public Character CreateCharacter()
        {
            Console.Write("Enter a character name: ");
            string name = Console.ReadLine();
            if (name == null || name.Trim() == "")
            {
                name = "Unnamed Wanderer";
            }

            Race race = ChooseRace();
            Dictionary<string, int> startingAttributes = AssignAttributes();

            Character character = new Character(name, race, startingAttributes);

            AssignSkills(character);
            character.CalculateDerivedStats();

            Console.WriteLine();
            Console.WriteLine(character.GetName() + " the " + race.GetName() + " has been created!");
            return character;
        }

        private Race ChooseRace()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a race:");
            for (int i = 0; i < RaceNames.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + RaceNames[i]);
            }
            Console.Write("Choice: ");
            int choice = ReadIntInRange(1, RaceNames.Length);
            string chosenName = RaceNames[choice - 1];

            return RaceFactory.CreateByName(chosenName);
        }

        private Dictionary<string, int> AssignAttributes()
        {
            Dictionary<string, int> attributes = new Dictionary<string, int>();
            foreach (string name in AttributeNames)
            {
                attributes[name] = 40;
            }

            int pointsRemaining = 30;
            Console.WriteLine();
            Console.WriteLine("All attributes start at 40. Distribute 30 extra points.");
            while (pointsRemaining > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Points remaining: " + pointsRemaining);
                Console.WriteLine(string.Join(", ", AttributeNames));
                Console.Write("Attribute to increase: ");

                string input = Console.ReadLine();
                if (input == null)
                {
                    input = "";
                }
                string chosen = input.Trim().ToUpper();

                bool isValidAttribute = false;
                foreach (string name in AttributeNames)
                {
                    if (name == chosen)
                    {
                        isValidAttribute = true;
                    }
                }

                if (!isValidAttribute)
                {
                    Console.WriteLine("Not a recognized attribute. Try again.");
                    continue;
                }

                Console.Write("How many points to add? ");
                int amount = ReadIntInRange(1, pointsRemaining);

                int newValue = attributes[chosen] + amount;
                if (newValue > 65)
                {
                    Console.WriteLine("Attributes cannot exceed 65 before racial bonuses. Try a smaller amount.");
                    continue;
                }

                attributes[chosen] = newValue;
                pointsRemaining = pointsRemaining - amount;
            }

            return attributes;
        }

        private void AssignSkills(Character character)
        {
            List<string> allSkillNames = new List<string>();
            foreach (string name in SkillDefinitions.All.Keys)
            {
                allSkillNames.Add(name);
            }

            List<string> majorSkills = ChooseSkillSet(allSkillNames, "Major", 5);

            List<string> remainingAfterMajor = new List<string>();
            foreach (string name in allSkillNames)
            {
                if (!majorSkills.Contains(name))
                {
                    remainingAfterMajor.Add(name);
                }
            }

            List<string> minorSkills = ChooseSkillSet(remainingAfterMajor, "Minor", 5);

            foreach (string skillName in allSkillNames)
            {
                string[] governingNames = SkillDefinitions.All[skillName];
                Attribute[] governingAttributes = new Attribute[governingNames.Length];
                for (int i = 0; i < governingNames.Length; i++)
                {
                    governingAttributes[i] = character.GetAttribute(governingNames[i]);
                }

                int startingValue;
                if (majorSkills.Contains(skillName))
                {
                    startingValue = 40;
                }
                else if (minorSkills.Contains(skillName))
                {
                    startingValue = 25;
                }
                else
                {
                    startingValue = 5;
                }

                character.AddSkill(new Skill(skillName, startingValue, governingAttributes));
            }
        }

        private List<string> ChooseSkillSet(List<string> availableSkills, string tierName, int countToChoose)
        {
            List<string> chosen = new List<string>();
            Console.WriteLine();
            Console.WriteLine("Choose " + countToChoose + " " + tierName + " skills from the list below:");
            Console.WriteLine(string.Join(", ", availableSkills));

            while (chosen.Count < countToChoose)
            {
                Console.Write(tierName + " skill #" + (chosen.Count + 1) + ": ");
                string input = Console.ReadLine();
                if (input == null)
                {
                    input = "";
                }
                input = input.Trim();

                string match = null;
                foreach (string skillName in availableSkills)
                {
                    if (string.Equals(skillName, input, StringComparison.OrdinalIgnoreCase))
                    {
                        match = skillName;
                    }
                }

                if (match == null || chosen.Contains(match))
                {
                    Console.WriteLine("Not a valid, unused skill from the list. Try again.");
                    continue;
                }

                chosen.Add(match);
            }

            return chosen;
        }

        public void DisplayCharacterSheet(Character character)
        {
            if (character == null)
            {
                Console.WriteLine();
                Console.WriteLine("No character created yet. Choose option 1 first.");
                return;
            }
            _printer.DisplaySheet(character);
        }

        public void RunSkillCheckDemo(Character character)
        {
            if (character == null)
            {
                Console.WriteLine();
                Console.WriteLine("No character created yet. Choose option 1 first.");
                return;
            }

            List<Skill> skills = character.GetAllSkills();

            List<string> skillNames = new List<string>();
            foreach (Skill skill in skills)
            {
                skillNames.Add(skill.GetName());
            }

            Console.WriteLine();
            Console.WriteLine("Choose a skill to check:");
            Console.WriteLine(string.Join(", ", skillNames));
            Console.Write("Skill: ");

            string input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim();

            Skill chosenSkill = null;
            foreach (Skill skill in skills)
            {
                if (string.Equals(skill.GetName(), input, StringComparison.OrdinalIgnoreCase))
                {
                    chosenSkill = skill;
                }
            }

            if (chosenSkill == null)
            {
                Console.WriteLine("Not a valid skill.");
                return;
            }

            Console.Write("Difficulty modifier (0 = normal, positive = harder, negative = easier): ");
            int difficulty = ReadInt();

            SkillCheck check = new SkillCheck(character, chosenSkill, difficulty);
            Console.WriteLine(check.PerformCheck());
        }

        public void SaveCharacterToFile(Character character)
        {
            if (character == null)
            {
                Console.WriteLine();
                Console.WriteLine("No character created yet. Choose option 1 first.");
                return;
            }

            Console.Write("Enter a filename to save to (example: boblin_the_goblin.txt): ");
            string filename = Console.ReadLine();
            if (filename == null || filename.Trim() == "")
            {
                filename = "character.txt";
            }
            else
            {
                filename = filename.Trim();
            }

            _fileManager.SaveCharacter(character, filename);
        }

        public Character LoadCharacterFromFile()
        {
            Console.Write("Enter a filename to load from (example: aldric.txt): ");
            string filename = Console.ReadLine();
            if (filename == null)
            {
                filename = "";
            }
            filename = filename.Trim();

            Character loadedCharacter = _fileManager.LoadCharacter(filename);
            if (loadedCharacter != null)
            {
                Console.WriteLine("Loaded " + loadedCharacter.GetName() + " from " + filename);
            }
            return loadedCharacter;
        }

        private int ReadInt()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Please enter a whole number: ");
            }
            return value;
        }

        private int ReadIntInRange(int min, int max)
        {
            int value = ReadInt();
            while (value < min || value > max)
            {
                Console.Write("Please enter a number between " + min + " and " + max + ": ");
                value = ReadInt();
            }
            return value;
        }
    }
}
