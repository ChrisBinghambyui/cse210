using System;

class CombatEngine
{
    private Enemy _currentEnemy;
    private DateTime _enemySpawnTime;
    private DateTime _lastTickTime;
    private bool _enemyDefeated;
    private bool _rewardClaimed;

    public CombatEngine()
    {
        _currentEnemy = null;
        _enemySpawnTime = DateTime.MinValue;
        _lastTickTime = DateTime.MinValue;
        _enemyDefeated = false;
        _rewardClaimed = false;
    }

    public CombatEngine(Enemy enemy, DateTime spawnTime, DateTime lastTick, bool defeated, bool rewardClaimed)
    {
        _currentEnemy = enemy;
        _enemySpawnTime = spawnTime;
        _lastTickTime = lastTick;
        _enemyDefeated = defeated;
        _rewardClaimed = rewardClaimed;
    }

    public Enemy GetCurrentEnemy() { return _currentEnemy; }
    public bool HasActiveEnemy() { return _currentEnemy != null && !_enemyDefeated && !_rewardClaimed; }

    public void SpawnEnemy(int playerLevel)
    {
        _currentEnemy = EnemyGenerator.Generate(playerLevel);
        _enemySpawnTime = DateTime.Now;
        _lastTickTime = DateTime.Now;
        _enemyDefeated = false;
        _rewardClaimed = false;
        Console.WriteLine("A threat approaches your village!");
        Console.WriteLine(_currentEnemy.GetStatusString());
    }

    public void CheckAndSpawn(int playerLevel)
    {
        if (_currentEnemy == null || _rewardClaimed)
        {
            DateTime lastSpawn = _enemySpawnTime == DateTime.MinValue ? DateTime.MinValue : _enemySpawnTime;
            if ((DateTime.Now - lastSpawn).TotalHours >= 24)
            {
                SpawnEnemy(playerLevel);
            }
        }
    }

    public void ProcessElapsedTime(int villagePower, int libraryBypass)
    {
        if (_currentEnemy == null || _enemyDefeated) return;

        DateTime now = DateTime.Now;
        double hoursElapsed = (now - _lastTickTime).TotalHours;
        int ticks = (int)hoursElapsed;

        if (ticks > 0)
        {
            for (int i = 0; i < ticks; i++)
            {
                _currentEnemy.ApplyHourlyDamage(villagePower, libraryBypass);
            }
            _lastTickTime = _lastTickTime.AddHours(ticks);

            if (_currentEnemy.IsDefeated())
            {
                _enemyDefeated = true;
            }
        }
    }

    public void NotifyGoalCompleted(GoalType type)
    {
        if (_currentEnemy == null || _enemyDefeated) return;
        if (_currentEnemy.GetRequiredGoalType() == type || type == GoalType.General)
        {
            _currentEnemy.RecordGoalProgress();
            if (_currentEnemy.IsGoalRequirementMet())
            {
                Console.WriteLine("Goal requirement met! Your village now fights the " + _currentEnemy.GetName() + " in earnest.");
            }
        }
    }

    public int[] ClaimVictoryReward()
    {
        if (!_enemyDefeated || _rewardClaimed) return new int[] { 0, 0 };
        _rewardClaimed = true;
        return new int[] { _currentEnemy.GetXpReward(), _currentEnemy.GetGoldReward() };
    }

    public int[] ApplyDefeat()
    {
        int xpLoss = Math.Max(10, _currentEnemy.GetXpReward() / 5);
        int goldLoss = Math.Max(5, _currentEnemy.GetGoldReward() / 5);
        _rewardClaimed = true;
        return new int[] { xpLoss, goldLoss };
    }

    public void DisplayThreat()
    {
        Console.WriteLine();
        Console.WriteLine("=== Active Threat ===");
        if (_currentEnemy == null)
        {
            Console.WriteLine("No threat currently. Your village is at peace.");
        }
        else if (_enemyDefeated && !_rewardClaimed)
        {
            Console.WriteLine("The " + _currentEnemy.GetName() + " has been defeated! Claim your reward.");
        }
        else if (_rewardClaimed)
        {
            Console.WriteLine("The last threat has passed. A new one comes tomorrow.");
        }
        else
        {
            Console.WriteLine(_currentEnemy.GetStatusString());
        }
    }

    public string Encode()
    {
        if (_currentEnemy == null)
        {
            return "none|" + _enemySpawnTime.Ticks + "|" + _lastTickTime.Ticks + "|" + _enemyDefeated + "|" + _rewardClaimed;
        }
        return _currentEnemy.Encode() + "~" + _enemySpawnTime.Ticks + "~" + _lastTickTime.Ticks + "~" + _enemyDefeated + "~" + _rewardClaimed;
    }
}
