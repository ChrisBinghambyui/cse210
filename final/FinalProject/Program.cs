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

        // Rulebook Chapter 11, Step 2: "No Attribute may be below 10 ... before racial bonuses."
        private const int AttributeMinimum = 10;
        private const int AttributeMaximum = 65;

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

            AssignSkills(character, race);
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

            Race chosenRace = null;
            while (chosenRace == null)
            {
                Console.Write("Choice: ");
                int choice = ReadIntInRange(1, RaceNames.Length);
                string chosenName = RaceNames[choice - 1];
                Race candidate = RaceFactory.CreateByName(chosenName);

                Console.WriteLine();
                Console.WriteLine(candidate.GetName());
                Console.WriteLine(candidate.GetBonusSummary());
                Console.WriteLine();
                Console.WriteLine(candidate.GetDescription());
                Console.WriteLine();
                Console.Write("Confirm this race? (yes/no): ");
                bool confirmed = ReadYesNo();

                if (confirmed)
                {
                    chosenRace = candidate;
                }
            }

            return chosenRace;
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

            bool confirmed = false;
            while (!confirmed)
            {
                Console.WriteLine();
                Console.WriteLine("Current Attributes:");
                foreach (string name in AttributeNames)
                {
                    Console.WriteLine("  " + name + " " + attributes[name]);
                }
                Console.WriteLine("Points remaining: " + pointsRemaining);

                Console.WriteLine();
                Console.WriteLine("1. Add points to an attribute");
                Console.WriteLine("2. Remove points from an attribute");
                Console.WriteLine("3. Confirm and continue");
                Console.Write("Choose an option: ");
                int choice = ReadIntInRange(1, 3);

                if (choice == 1)
                {
                    pointsRemaining = AddAttributePoints(attributes, pointsRemaining);
                }
                else if (choice == 2)
                {
                    pointsRemaining = RemoveAttributePoints(attributes, pointsRemaining);
                }
                else if (choice == 3)
                {
                    if (pointsRemaining > 0)
                    {
                        Console.WriteLine("You still have " + pointsRemaining + " points to distribute first.");
                    }
                    else
                    {
                        confirmed = true;
                    }
                }
            }

            return attributes;
        }

        private int AddAttributePoints(Dictionary<string, int> attributes, int pointsRemaining)
        {
            if (pointsRemaining <= 0)
            {
                Console.WriteLine("You have no points left. Remove some from another attribute first.");
                return pointsRemaining;
            }

            Console.Write("Which attribute do you want to add points to? ");
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
                Console.WriteLine("Not a recognized attribute.");
                return pointsRemaining;
            }

            Console.Write("How many points to add? ");
            int amount = ReadIntInRange(1, pointsRemaining);

            int newValue = attributes[chosen] + amount;
            if (newValue > AttributeMaximum)
            {
                Console.WriteLine("Attributes cannot exceed " + AttributeMaximum + " before racial bonuses. Try a smaller amount.");
                return pointsRemaining;
            }

            attributes[chosen] = newValue;
            return pointsRemaining - amount;
        }

        private int RemoveAttributePoints(Dictionary<string, int> attributes, int pointsRemaining)
        {
            Console.Write("Which attribute do you want to remove points from? ");
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
                Console.WriteLine("Not a recognized attribute.");
                return pointsRemaining;
            }

            int removable = attributes[chosen] - AttributeMinimum;
            if (removable <= 0)
            {
                Console.WriteLine("That attribute is already at the minimum of " + AttributeMinimum + ".");
                return pointsRemaining;
            }

            Console.Write("How many points to remove (up to " + removable + ")? ");
            int amount = ReadIntInRange(1, removable);

            attributes[chosen] = attributes[chosen] - amount;
            return pointsRemaining + amount;
        }

        private void AssignSkills(Character character, Race race)
        {
            List<string> majorSkills = new List<string>();
            List<string> minorSkills = new List<string>();

            bool confirmed = false;
            while (!confirmed)
            {
                majorSkills = ChooseSkillGroup("Major", 5, new List<string>());
                minorSkills = ChooseSkillGroup("Minor", 5, majorSkills);

                Console.WriteLine();
                Console.WriteLine("Major Skills (start at 40): " + string.Join(", ", majorSkills));
                Console.WriteLine("Minor Skills (start at 25): " + string.Join(", ", minorSkills));
                Console.WriteLine("All other skills start at 5.");
                Console.Write("Confirm these skills? (yes/no): ");
                confirmed = ReadYesNo();
            }

            List<string> allSkillNames = new List<string>();
            foreach (string name in SkillDefinitions.All.Keys)
            {
                allSkillNames.Add(name);
            }

            List<string> miscSkills = new List<string>();

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
                    miscSkills.Add(skillName);
                }

                character.AddSkill(new Skill(skillName, startingValue, governingAttributes));
            }

            // Rulebook Step 3: "Apply racial skill bonuses. Then, distribute 5 points among
            // the Major Skills, 5 among the Minor Skills, and 5 among the Miscellaneous skills."
            race.ApplySkillBonuses(character);

            DistributeSkillPoints(character, "Major", majorSkills, 5);
            DistributeSkillPoints(character, "Minor", minorSkills, 5);
            DistributeSkillPoints(character, "Miscellaneous", miscSkills, 5);
        }

        private void DistributeSkillPoints(Character character, string tierName, List<string> skillNames, int totalPoints)
        {
            int pointsRemaining = totalPoints;
            Dictionary<string, int> spentThisTier = new Dictionary<string, int>();
            foreach (string name in skillNames)
            {
                spentThisTier[name] = 0;
            }

            Console.WriteLine();
            Console.WriteLine("Distribute " + totalPoints + " points among your " + tierName + " skills.");
            Console.WriteLine("(A skill cannot rise above its governing Attribute's cap.)");

            bool confirmed = false;
            while (!confirmed)
            {
                Console.WriteLine();
                Console.WriteLine(tierName + " skills:");
                foreach (string name in skillNames)
                {
                    Skill skillInGroup = character.GetSkill(name);
                    Console.WriteLine("  " + name + ": " + skillInGroup.GetValue() + " (cap " + skillInGroup.GetCap() + ")");
                }
                Console.WriteLine("Points remaining: " + pointsRemaining);

                Console.WriteLine();
                Console.WriteLine("1. Add points to a skill");
                Console.WriteLine("2. Remove points from a skill");
                Console.WriteLine("3. Confirm and continue");
                Console.Write("Choose an option: ");
                int choice = ReadIntInRange(1, 3);

                if (choice == 1)
                {
                    pointsRemaining = AddSkillPoints(character, tierName, skillNames, spentThisTier, pointsRemaining);
                }
                else if (choice == 2)
                {
                    pointsRemaining = RemoveSkillPoints(character, skillNames, spentThisTier, pointsRemaining);
                }
                else if (choice == 3)
                {
                    confirmed = true;
                }
            }
        }

        private int AddSkillPoints(Character character, string tierName, List<string> skillNames,
            Dictionary<string, int> spentThisTier, int pointsRemaining)
        {
            if (pointsRemaining <= 0)
            {
                Console.WriteLine("You have no " + tierName + " points left to spend.");
                return pointsRemaining;
            }

            Console.Write("Which skill do you want to add points to? ");
            string input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim();

            Skill chosen = FindSkillInGroup(character, skillNames, input);
            if (chosen == null)
            {
                Console.WriteLine("That's not one of your " + tierName + " skills.");
                return pointsRemaining;
            }

            int roomUnderCap = chosen.GetCap() - chosen.GetValue();
            if (roomUnderCap <= 0)
            {
                Console.WriteLine(chosen.GetName() + " is already at its Attribute cap (" +
                    chosen.GetCap() + ") and cannot rise further until that Attribute improves.");
                return pointsRemaining;
            }

            int maxAmount = Math.Min(pointsRemaining, roomUnderCap);
            Console.Write("How many points to add (up to " + maxAmount + ")? ");
            int amount = ReadIntInRange(1, maxAmount);

            chosen.IncreaseSkill(amount);
            spentThisTier[chosen.GetName()] = spentThisTier[chosen.GetName()] + amount;
            return pointsRemaining - amount;
        }

        private int RemoveSkillPoints(Character character, List<string> skillNames,
            Dictionary<string, int> spentThisTier, int pointsRemaining)
        {
            Console.Write("Which skill do you want to remove points from? ");
            string input = Console.ReadLine();
            if (input == null)
            {
                input = "";
            }
            input = input.Trim();

            Skill chosen = FindSkillInGroup(character, skillNames, input);
            if (chosen == null)
            {
                Console.WriteLine("That's not one of your skills in this tier.");
                return pointsRemaining;
            }

            int alreadySpent = spentThisTier[chosen.GetName()];
            if (alreadySpent <= 0)
            {
                Console.WriteLine("You haven't put any of these points into " + chosen.GetName() + " yet.");
                return pointsRemaining;
            }

            Console.Write("How many points to remove (up to " + alreadySpent + ")? ");
            int amount = ReadIntInRange(1, alreadySpent);

            chosen.SetValue(chosen.GetValue() - amount);
            spentThisTier[chosen.GetName()] = alreadySpent - amount;
            return pointsRemaining + amount;
        }

        private Skill FindSkillInGroup(Character character, List<string> skillNames, string input)
        {
            foreach (string name in skillNames)
            {
                if (string.Equals(name, input, StringComparison.OrdinalIgnoreCase))
                {
                    return character.GetSkill(name);
                }
            }
            return null;
        }

        private void PrintSkillGroups()
        {
            Console.WriteLine("1: Warrior Skills");
            for (int i = 0; i < SkillDefinitions.WarriorSkills.Length; i++)
            {
                string skillName = SkillDefinitions.WarriorSkills[i];
                string description = SkillDefinitions.Descriptions[skillName];
                Console.WriteLine("  1" + (i + 1) + " - " + skillName + " (" + description + ")");
            }
            Console.WriteLine("2: Mage Skills");
            for (int i = 0; i < SkillDefinitions.MageSkills.Length; i++)
            {
                string skillName = SkillDefinitions.MageSkills[i];
                string description = SkillDefinitions.Descriptions[skillName];
                Console.WriteLine("  2" + (i + 1) + " - " + skillName + " (" + description + ")");
            }
            Console.WriteLine("3: Thief Skills");
            for (int i = 0; i < SkillDefinitions.ThiefSkills.Length; i++)
            {
                string skillName = SkillDefinitions.ThiefSkills[i];
                string description = SkillDefinitions.Descriptions[skillName];
                Console.WriteLine("  3" + (i + 1) + " - " + skillName + " (" + description + ")");
            }
        }

        private string GetSkillNameFromCode(string code)
        {
            if (code.Length != 2)
            {
                return null;
            }

            char groupChar = code[0];
            char positionChar = code[1];

            string[] group;
            if (groupChar == '1')
            {
                group = SkillDefinitions.WarriorSkills;
            }
            else if (groupChar == '2')
            {
                group = SkillDefinitions.MageSkills;
            }
            else if (groupChar == '3')
            {
                group = SkillDefinitions.ThiefSkills;
            }
            else
            {
                return null;
            }

            int position;
            bool isNumber = int.TryParse(positionChar.ToString(), out position);
            if (!isNumber || position < 1 || position > group.Length)
            {
                return null;
            }

            return group[position - 1];
        }

        private List<string> ChooseSkillGroup(string tierName, int countToChoose, List<string> alreadyChosen)
        {
            List<string> chosen = new List<string>();

            Console.WriteLine();
            Console.WriteLine("Choose " + countToChoose + " " + tierName + " skills.");
            Console.WriteLine("Enter a 2-digit code: first digit is the group, second digit is the skill number.");
            Console.WriteLine("Example: 13 = group 1 (Warrior), skill 3 in that list.");
            PrintSkillGroups();

            while (chosen.Count < countToChoose)
            {
                Console.Write(tierName + " skill #" + (chosen.Count + 1) + ": ");
                string input = Console.ReadLine();
                if (input == null)
                {
                    input = "";
                }
                input = input.Trim();

                string skillName = GetSkillNameFromCode(input);

                if (skillName == null)
                {
                    Console.WriteLine("Not a valid code. Try again.");
                    continue;
                }

                if (alreadyChosen.Contains(skillName) || chosen.Contains(skillName))
                {
                    Console.WriteLine("That skill has already been chosen. Try again.");
                    continue;
                }

                chosen.Add(skillName);
                Console.WriteLine("Added " + skillName + ".");
            }

            return chosen;
        }

        private bool ReadYesNo()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    input = "";
                }
                input = input.Trim().ToLower();

                if (input == "yes" || input == "y")
                {
                    return true;
                }
                if (input == "no" || input == "n")
                {
                    return false;
                }

                Console.Write("Please enter yes or no: ");
            }
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

            Console.Write("Enter a name for this character sheet (no extension needed): ");
            string name = Console.ReadLine();
            if (name == null || name.Trim() == "")
            {
                name = "character";
            }
            else
            {
                name = name.Trim();
            }

            _fileManager.SaveCharacter(character, name);
        }

        public Character LoadCharacterFromFile()
        {
            List<string> savedNames = _fileManager.ListSavedCharacterNames();

            if (savedNames.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No saved character sheets found.");
                return null;
            }

            Console.WriteLine();
            Console.WriteLine("Saved character sheets:");
            for (int i = 0; i < savedNames.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + savedNames[i]);
            }

            Console.Write("Enter the number of the character sheet to load: ");
            int choice = ReadIntInRange(1, savedNames.Count);
            string chosenName = savedNames[choice - 1];

            Character loadedCharacter = _fileManager.LoadCharacter(chosenName);
            if (loadedCharacter != null)
            {
                Console.WriteLine("Loaded " + loadedCharacter.GetName() + ".");
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
