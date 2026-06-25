using System;

class Enemy
{
    private string _name;
    private int _maxHealth;
    private int _currentHealth;
    private GoalType _requiredGoalType;
    private int _goalsRequired;
    private int _goalsCompleted;
    private int _damageResistance;
    private int _goldReward;
    private int _xpReward;

    public Enemy(string name, int maxHealth, GoalType requiredGoalType, int goalsRequired,
        int damageResistance, int goldReward, int xpReward)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        _requiredGoalType = requiredGoalType;
        _goalsRequired = goalsRequired;
        _goalsCompleted = 0;
        _damageResistance = damageResistance;
        _goldReward = goldReward;
        _xpReward = xpReward;
    }

    public Enemy(string name, int maxHealth, int currentHealth, GoalType requiredGoalType,
        int goalsRequired, int goalsCompleted, int damageResistance, int goldReward, int xpReward)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = currentHealth;
        _requiredGoalType = requiredGoalType;
        _goalsRequired = goalsRequired;
        _goalsCompleted = goalsCompleted;
        _damageResistance = damageResistance;
        _goldReward = goldReward;
        _xpReward = xpReward;
    }

    public string GetName() { return _name; }
    public int GetCurrentHealth() { return _currentHealth; }
    public int GetMaxHealth() { return _maxHealth; }
    public GoalType GetRequiredGoalType() { return _requiredGoalType; }
    public int GetGoalsRequired() { return _goalsRequired; }
    public int GetGoalsCompleted() { return _goalsCompleted; }
    public int GetGoldReward() { return _goldReward; }
    public int GetXpReward() { return _xpReward; }
    public bool IsDefeated() { return _currentHealth <= 0; }

    public void RecordGoalProgress()
    {
        _goalsCompleted = Math.Min(_goalsCompleted + 1, _goalsRequired);
    }

    public bool IsGoalRequirementMet() { return _goalsCompleted >= _goalsRequired; }

    public void ApplyHourlyDamage(int villagePower, int libraryBypassBonus)
    {
        if (!IsGoalRequirementMet()) return;

        int effectivePower = villagePower;
        int resistance = Math.Max(0, _damageResistance - libraryBypassBonus);
        int damage = Math.Max(1, effectivePower - resistance);
        _currentHealth = Math.Max(0, _currentHealth - damage);
    }

    public string GetStatusString()
    {
        int bars = (int)(10.0 * _currentHealth / _maxHealth);
        string healthBar = "[" + new string('#', bars) + new string('-', 10 - bars) + "]";
        string goalStatus = "Goals needed: " + _goalsCompleted + "/" + _goalsRequired + " " + _requiredGoalType;
        string locked = IsGoalRequirementMet() ? "" : " [LOCKED - complete required goals first]";
        return "  " + _name + "\n  HP: " + healthBar + " " + _currentHealth + "/" + _maxHealth
            + "\n  " + goalStatus + locked
            + "\n  Reward on defeat: +" + _xpReward + " XP, +" + _goldReward + " gold";
    }

    public string Encode()
    {
        return _name + "|" + _maxHealth + "|" + _currentHealth + "|" + _requiredGoalType
            + "|" + _goalsRequired + "|" + _goalsCompleted + "|" + _damageResistance
            + "|" + _goldReward + "|" + _xpReward;
    }
}
