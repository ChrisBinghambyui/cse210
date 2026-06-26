// Since gameifying anything scratches my game dev brain, i kinda want nuts here. Once i got the minimum reqs in by having it interpret the three different goal types, I wanted to add a system of generating generic ones for the user alogside the ones they set for themselves. To incentivize them both I used two different 'currencies', gold and xp, which both provide upgrades along different paths. The user could go nuts on personal goals, but without completing the generated goals for the villagers they'll be left with no gold. The user needs to do both to steadily progress. And for something to use these in-game upgrades for, each day there will be an enemy to defeat. They can start dealing damage once they complete the mandated requirement (like they can't deal damage until they've turned in 3 spiritual goals). Then, each hour the village will deal damage according to bought with gold. If they defeat it, they get a nice big prize. Originally I wanted to add like a district building system, and achievements and a more complex enemy fighting system, but then I realized I'm getting lost in the sauce so here we are.


using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        GoalManager gm = new GoalManager();
        FileManager fm = new FileManager();

        Console.WriteLine("=== Eternal Quest ===");
        Console.Write("Load a saved game? (y/n): ");
        string load = Console.ReadLine();
        if (load == "y" || load == "Y")
        {
            Console.Write("Filename: ");
            string filename = Console.ReadLine();
            try
            {
                gm = fm.LoadGoals(filename);
                Console.WriteLine("Loaded.");
            }
            catch
            {
                Console.WriteLine("Could not load file. Starting fresh.");
            }
        }

        gm.OnSessionStart();

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Eternal Quest | " + gm.GetHeader() + " ===");
            gm.DisplayThreat();
            Console.WriteLine();
            Console.WriteLine("1.  Complete a goal");
            Console.WriteLine("2.  View personal goals");
            Console.WriteLine("3.  Create a personal goal");
            Console.WriteLine("4.  Missives and Errands");
            Console.WriteLine("5.  Village buildings");
            Console.WriteLine("6.  Villagers");
            Console.WriteLine("7.  Recruit a villager");
            Console.WriteLine("8.  Save");
            Console.WriteLine("9.  Quit");
            Console.Write("Choose: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            if (choice == 1) gm.CompleteGoal();
            else if (choice == 2) gm.DisplayGoals();
            else if (choice == 3) CreateGoal(gm);
            else if (choice == 4) gm.GetBoard().Display();
            else if (choice == 5) gm.GetTown().UpgradeMenu();
            else if (choice == 6) gm.ViewVillagers();
            else if (choice == 7) gm.RecruitVillager();
            else if (choice == 8)
            {
                Console.Write("Filename: ");
                string filename = Console.ReadLine();
                fm.SaveGoals(gm, filename);
                Console.WriteLine("Saved.");
            }
            else if (choice == 9)
            {
                running = false;
                Console.WriteLine("Farewell, adventurer.");
            }
        }
    }

    static void CreateGoal(GoalManager gm)
    {
        Console.WriteLine();
        Console.WriteLine("Goal type:");
        Console.WriteLine("1. Simple (complete once)");
        Console.WriteLine("2. Eternal (repeating, never done)");
        Console.WriteLine("3. Checklist (complete a set number of times)");
        Console.Write("Choose: ");
        int type;
        int.TryParse(Console.ReadLine(), out type);

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        Console.Write("XP reward: ");
        int reward;
        int.TryParse(Console.ReadLine(), out reward);

        Console.WriteLine("Goal category:");
        Console.WriteLine("1. Physical  2. Social  3. Educational  4. Spiritual  5. General");
        Console.Write("Choose: ");
        int cat;
        int.TryParse(Console.ReadLine(), out cat);
        GoalType goalType = GoalType.General;
        if (cat == 1) goalType = GoalType.Physical;
        else if (cat == 2) goalType = GoalType.Social;
        else if (cat == 3) goalType = GoalType.Educational;
        else if (cat == 4) goalType = GoalType.Spiritual;

        Goal newGoal = null;
        if (type == 1)
        {
            newGoal = new SimpleGoal(name, desc, reward, goalType);
        }
        else if (type == 2)
        {
            newGoal = new EternalGoal(name, desc, reward, goalType);
        }
        else if (type == 3)
        {
            Console.Write("Times to complete: ");
            int limit;
            int.TryParse(Console.ReadLine(), out limit);
            Console.Write("Bonus XP on completion: ");
            int bonus;
            int.TryParse(Console.ReadLine(), out bonus);
            newGoal = new ChecklistGoal(name, desc, reward, limit, bonus, goalType);
        }

        if (newGoal != null)
        {
            gm.AddGoal(newGoal);
            Console.WriteLine("Goal added.");
        }
    }
}