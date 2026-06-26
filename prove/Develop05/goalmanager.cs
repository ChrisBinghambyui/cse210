using System;
using System.Collections.Generic;

class GoalManager
{
    private List<Goal> _goals;
    private int _xp;
    private int _level;
    private TownManager _town;
    private VillagerManager _villagers;
    private CommissionBoard _board;
    private CombatEngine _combat;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _xp = 0;
        _level = 1;
        _town = new TownManager();
        _villagers = new VillagerManager();
        _villagers.RecruitVillager();
        _villagers.RecruitVillager();
        _board = new CommissionBoard();
        _combat = new CombatEngine();
    }

    public GoalManager(List<Goal> goals, int xp, int level, TownManager town,
        VillagerManager villagers, CommissionBoard board, CombatEngine combat)
    {
        _goals = goals;
        _xp = xp;
        _level = level;
        _town = town;
        _villagers = villagers;
        _board = board;
        _combat = combat;
    }

    public List<Goal> GetGoals() { return _goals; }
    public int GetXp() { return _xp; }
    public int GetLevel() { return _level; }
    public TownManager GetTown() { return _town; }
    public VillagerManager GetVillagers() { return _villagers; }
    public CommissionBoard GetBoard() { return _board; }
    public CombatEngine GetCombat() { return _combat; }

    public void OnSessionStart()
    {
        int villagePower = 10 + _town.GetBlacksmithCombatBonus();
        _combat.ProcessElapsedTime(villagePower);
        _combat.CheckAndSpawn(_level);
        _board.Refresh(_villagers.GetVillagerNames());
    }

    public void AddGoal(Goal goal) { _goals.Add(goal); }

    public void CompleteGoal()
    {
        if (_goals.Count == 0 && _board.GetAllActive().Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("--- Personal Goals ---");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + _goals[i].GetStatus());
        }

        List<Commission> commissions = _board.GetAllActive();
        if (commissions.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("--- Missives and Errands ---");
            for (int i = 0; i < commissions.Count; i++)
            {
                Console.WriteLine((_goals.Count + i + 1) + ". " + commissions[i].GetStatus());
            }
        }

        Console.Write("Which goal did you complete? (number, 0 to cancel): ");
        int index;
        int.TryParse(Console.ReadLine(), out index);
        if (index == 0) return;
        index -= 1;

        if (index < _goals.Count)
        {
            // Personal goals: XP only
            Goal completed = _goals[index];
            int xpEarned = completed.RecordEvent();
            xpEarned = ApplyXpBonus(completed, xpEarned);
            if (xpEarned > 0)
            {
                _xp += xpEarned;
                _combat.NotifyGoalCompleted(completed.GetGoalType());
                Console.WriteLine($"+{xpEarned} XP");
                CheckLevelUp();
                if (_combat.GetCurrentEnemy() != null && _combat.GetCurrentEnemy().IsDefeated())
                {
                    int[] rewards = _combat.ClaimVictoryReward();
                    _xp += rewards[0];
                    _town.AddGold(rewards[1]);
                    Console.WriteLine($"The {_combat.GetCurrentEnemy().GetName()} has fallen! +{rewards[0]} XP, +{rewards[1]} gold");
                }
            }
        }
        else
        {
            // Commissions: gold only
            int commIndex = index - _goals.Count;
            Commission c = _board.GetCommissionByIndex(commIndex);
            if (c == null) { Console.WriteLine("Invalid choice."); return; }
            c.RecordEvent();
            int goldEarned = c.GetGoldReward();

            if (_board.CheckDailySetComplete())
            {
                goldEarned += _board.GetDailySetBonus();
                Console.WriteLine("All daily missives complete! Bonus gold!");
            }
            if (_board.CheckWeeklySetComplete())
            {
                goldEarned += _board.GetWeeklySetBonus();
                Console.WriteLine("All weekly errands complete! Bonus gold!");
            }

            _town.AddGold(goldEarned);
            _combat.NotifyGoalCompleted(c.GetGoalType());
            Console.WriteLine($"+{goldEarned} gold");
        }
    }

    private int ApplyXpBonus(Goal goal, int baseXp)
    {
        int bonus = 0;
        if (goal is SimpleGoal) bonus = _town.GetBlacksmithBonus();
        else if (goal is EternalGoal) bonus = _town.GetLibraryBonus();
        else if (goal is ChecklistGoal) bonus = _town.GetTavernBonus();

        if (bonus == 0) return baseXp;
        return baseXp + (baseXp * bonus / 100);
    }

    private void CheckLevelUp()
    {
        int newLevel = (_xp / 1000) + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            int goldBonus = _town.GetTavernBonus() >= 75 ? 100 : 50;
            _town.AddGold(goldBonus);
            Console.WriteLine("*** Level " + _level + "! Your village grows! +" + goldBonus + " gold ***");
            _combat.CheckAndSpawn(_level);
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine();
        Console.WriteLine("--- Personal Goals ---");
        if (_goals.Count == 0)
        {
            Console.WriteLine("None yet.");
        }
        else
        {
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + _goals[i].GetStatus());
            }
        }
    }

    public void DisplayThreat()
    {
        _combat.DisplayThreat();
    }

    public void ViewVillagers()
    {
        _villagers.Display();
        if (_villagers.GetCount() == 0) return;

        Console.Write("Interact with a villager? (number, 0 to skip): ");
        int choice;
        int.TryParse(Console.ReadLine(), out choice);
        if (choice == 0) return;

        Commission quest = _villagers.InteractWithVillager(choice - 1);
        if (quest != null)
        {
            Console.Write("Accept this personal quest? (y/n): ");
            string confirm = Console.ReadLine();
            if (confirm == "y" || confirm == "Y")
            {
                _goals.Add(quest);
                Console.WriteLine("Personal quest added to your goal list.");
            }
        }
    }

    public void RecruitVillager()
    {
        int cost = 25 + (_villagers.GetCount() * 10);
        Console.WriteLine("Recruiting a villager costs " + cost + " gold. You have " + _town.GetGold() + ".");
        Console.Write("Recruit? (y/n): ");
        string confirm = Console.ReadLine();
        if (confirm != "y" && confirm != "Y") return;
        if (!_town.SpendGold(cost))
        {
            Console.WriteLine("Not enough gold.");
            return;
        }
        _villagers.RecruitVillager();
    }

    public string GetHeader()
    {
        return "Level " + _level + " | XP: " + _xp + " | Gold: " + _town.GetGold()
            + " | Villagers: " + _villagers.GetCount() + "/" + _villagers.GetMaxVillagers();
    }
}