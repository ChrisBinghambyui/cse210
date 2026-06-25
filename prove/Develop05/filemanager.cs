using System;
using System.Collections.Generic;
using System.IO;

class FileManager
{
    public void SaveGoals(GoalManager gm, string filename)
    {
        using (StreamWriter w = new StreamWriter(filename))
        {
            w.WriteLine(gm.GetXp());
            w.WriteLine(gm.GetLevel());
            w.WriteLine(gm.GetTown().Encode());
            w.WriteLine(gm.GetExpansion().Encode());
            w.WriteLine(gm.GetVillagers().Encode());
            w.WriteLine(gm.GetCombat().Encode());

            string boardEncoded = gm.GetBoard().Encode();
            string[] boardLines = boardEncoded.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            w.WriteLine(boardLines.Length);
            foreach (string line in boardLines)
                w.WriteLine(line);

            List<Goal> goals = gm.GetGoals();
            w.WriteLine(goals.Count);
            foreach (Goal g in goals)
                w.WriteLine(g.Encode());
        }
    }

    public GoalManager LoadGoals(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        int i = 0;

        int xp = int.Parse(lines[i++]);
        int level = int.Parse(lines[i++]);

        TownManager town = DecodeTown(lines[i++]);
        ExpansionManager expansion = DecodeExpansion(lines, ref i);
        VillagerManager villagers = DecodeVillagers(lines, ref i);
        CombatEngine combat = DecodeCombat(lines[i++]);

        int boardLineCount = int.Parse(lines[i++]);
        string[] boardLines = new string[boardLineCount];
        for (int b = 0; b < boardLineCount; b++)
            boardLines[b] = lines[i++];
        CommissionBoard board = DecodeBoard(boardLines);

        int goalCount = int.Parse(lines[i++]);
        List<Goal> goals = new List<Goal>();
        for (int g = 0; g < goalCount; g++)
            goals.Add(DecodeGoal(lines[i++]));

        return new GoalManager(goals, xp, level, town, villagers, board, combat, expansion);
    }

    private TownManager DecodeTown(string line)
    {
        string[] parts = line.Split("|");
        int gold = int.Parse(parts[0]);
        List<int> tiers = new List<int>();
        for (int i = 1; i < parts.Length; i++)
            tiers.Add(int.Parse(parts[i]));
        return new TownManager(gold, tiers);
    }

    private ExpansionManager DecodeExpansion(string[] lines, ref int i)
    {
        int tier = int.Parse(lines[i++]);
        int quarterCount = int.Parse(lines[i++]);
        List<Quarter> quarters = new List<Quarter>();
        for (int q = 0; q < quarterCount; q++)
        {
            string[] parts = lines[i++].Split("|");
            string name = parts[0];
            QuarterSpecialty specialty = (QuarterSpecialty)Enum.Parse(typeof(QuarterSpecialty), parts[1]);
            int bonus = int.Parse(parts[2]);
            quarters.Add(new Quarter(name, specialty, bonus));
        }
        return new ExpansionManager(tier, quarters);
    }

    private VillagerManager DecodeVillagers(string[] lines, ref int i)
    {
        int maxVillagers = int.Parse(lines[i++]);
        int count = int.Parse(lines[i++]);
        List<Villager> villagers = new List<Villager>();
        for (int v = 0; v < count; v++)
        {
            string[] parts = lines[i++].Split("|");
            string first = parts[0];
            string last = parts[1];
            VillagerSpecialty specialty = (VillagerSpecialty)Enum.Parse(typeof(VillagerSpecialty), parts[2]);
            int tier = int.Parse(parts[3]);
            DateTime arrival = new DateTime(long.Parse(parts[4]));
            bool offeredQuest = bool.Parse(parts[5]);
            bool questComplete = bool.Parse(parts[6]);
            villagers.Add(new Villager(first, last, specialty, tier, arrival, offeredQuest, questComplete));
        }
        return new VillagerManager(villagers, maxVillagers);
    }

    private CombatEngine DecodeCombat(string line)
    {
        if (line.StartsWith("none|"))
        {
            string[] parts = line.Split("|");
            DateTime spawnTime = new DateTime(long.Parse(parts[1]));
            DateTime lastTick = new DateTime(long.Parse(parts[2]));
            bool defeated = bool.Parse(parts[3]);
            bool claimed = bool.Parse(parts[4]);
            return new CombatEngine(null, spawnTime, lastTick, defeated, claimed);
        }

        string[] segments = line.Split("~");
        string[] ep = segments[0].Split("|");
        string name = ep[0];
        int maxHp = int.Parse(ep[1]);
        int currentHp = int.Parse(ep[2]);
        GoalType reqType = (GoalType)Enum.Parse(typeof(GoalType), ep[3]);
        int goalsReq = int.Parse(ep[4]);
        int goalsDone = int.Parse(ep[5]);
        int resistance = int.Parse(ep[6]);
        int goldReward = int.Parse(ep[7]);
        int xpReward = int.Parse(ep[8]);

        Enemy enemy = new Enemy(name, maxHp, currentHp, reqType, goalsReq, goalsDone, resistance, goldReward, xpReward);
        DateTime spawn = new DateTime(long.Parse(segments[1]));
        DateTime tick = new DateTime(long.Parse(segments[2]));
        bool def = bool.Parse(segments[3]);
        bool rew = bool.Parse(segments[4]);
        return new CombatEngine(enemy, spawn, tick, def, rew);
    }

    private CommissionBoard DecodeBoard(string[] lines)
    {
        int i = 0;
        DateTime lastDaily = new DateTime(long.Parse(lines[i++]));
        DateTime lastWeekly = new DateTime(long.Parse(lines[i++]));

        int dailyCount = int.Parse(lines[i++]);
        List<Commission> daily = new List<Commission>();
        for (int d = 0; d < dailyCount; d++)
            daily.Add(DecodeCommission(lines[i++]));

        int weeklyCount = int.Parse(lines[i++]);
        List<Commission> weekly = new List<Commission>();
        for (int w = 0; w < weeklyCount; w++)
            weekly.Add(DecodeCommission(lines[i++]));

        return new CommissionBoard(daily, weekly, lastDaily, lastWeekly);
    }

    private Commission DecodeCommission(string line)
    {
        string[] p = line.Split("|");
        string name = p[1];
        string desc = p[2];
        int xp = int.Parse(p[3]);
        GoalType type = (GoalType)Enum.Parse(typeof(GoalType), p[4]);
        int gold = int.Parse(p[5]);
        CommissionFrequency freq = (CommissionFrequency)Enum.Parse(typeof(CommissionFrequency), p[6]);
        DateTime deadline = new DateTime(long.Parse(p[7]));
        bool complete = bool.Parse(p[8]);
        string source = p[9];
        Commission c = new Commission(name, desc, xp, gold, type, freq, deadline, source);
        if (complete) c.RecordEvent();
        return c;
    }

    private Goal DecodeGoal(string line)
    {
        string[] p = line.Split("|");
        string type = p[0];

        if (type == "Simple")
        {
            GoalType gt = (GoalType)Enum.Parse(typeof(GoalType), p[4]);
            return new SimpleGoal(p[1], p[2], int.Parse(p[3]), gt, bool.Parse(p[5]));
        }
        else if (type == "Eternal")
        {
            GoalType gt = (GoalType)Enum.Parse(typeof(GoalType), p[4]);
            return new EternalGoal(p[1], p[2], int.Parse(p[3]), gt);
        }
        else if (type == "Checklist")
        {
            GoalType gt = (GoalType)Enum.Parse(typeof(GoalType), p[4]);
            return new ChecklistGoal(p[1], p[2], int.Parse(p[3]), int.Parse(p[5]), int.Parse(p[6]), int.Parse(p[7]), gt);
        }
        else if (type == "Commission")
        {
            return DecodeCommission(line);
        }

        return new EternalGoal(p[1], p[2], int.Parse(p[3]));
    }
}
