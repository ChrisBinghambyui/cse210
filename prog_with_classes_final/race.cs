using System;
using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public abstract class Race
    {
        private string _name;
        private string _description;

        protected Race(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDescription()
        {
            return _description;
        }

        public abstract void ApplyRacialBonuses(Character character);

        public abstract void ApplySkillBonuses(Character character);

        public abstract string GetBonusSummary();

        protected string ChooseOneOfTwo(string promptMessage, string optionA, string optionB)
        {
            Console.WriteLine(promptMessage);
            Console.WriteLine("1. " + DescribeOption(optionA));
            Console.WriteLine("2. " + DescribeOption(optionB));
            Console.Write("Choice: ");
            int choice = ReadChoiceInRange(1, 2);
            if (choice == 1)
            {
                return optionA;
            }
            return optionB;
        }

        protected string ChooseOneFromList(string promptMessage, string[] options)
        {
            Console.WriteLine(promptMessage);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + DescribeOption(options[i]));
            }
            Console.Write("Choice: ");
            int choice = ReadChoiceInRange(1, options.Length);
            return options[choice - 1];
        }

        protected List<string> ChooseManyFromList(string promptMessage, string[] options, int countToChoose)
        {
            Console.WriteLine(promptMessage);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + DescribeOption(options[i]));
            }

            List<string> chosen = new List<string>();
            while (chosen.Count < countToChoose)
            {
                Console.Write("Choice #" + (chosen.Count + 1) + ": ");
                int choiceNumber = ReadChoiceInRange(1, options.Length);
                string selected = options[choiceNumber - 1];
                if (chosen.Contains(selected))
                {
                    Console.WriteLine("Already chosen. Pick a different one.");
                    continue;
                }
                chosen.Add(selected);
            }
            return chosen;
        }

        private string DescribeOption(string optionName)
        {
            if (SkillDefinitions.Descriptions.ContainsKey(optionName))
            {
                return optionName + " (" + SkillDefinitions.Descriptions[optionName] + ")";
            }
            return optionName;
        }

        protected int ReadChoiceInRange(int min, int max)
        {
            int value = 0;
            bool valid = false;
            while (!valid)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out value);
                if (isNumber && value >= min && value <= max)
                {
                    valid = true;
                }
                else
                {
                    Console.Write("Please enter a number between " + min + " and " + max + ": ");
                }
            }
            return value;
        }
    }
}
