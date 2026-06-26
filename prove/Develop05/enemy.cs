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
        : this(name, maxHealth, maxHealth, requiredGoalType, goalsRequired, 0, damageResistance, goldReward, xpReward) { }

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

    public string GetName() => _name;
    public int GetCurrentHealth() => _currentHealth;
    public int GetMaxHealth() => _maxHealth;
    public GoalType GetRequiredGoalType() => _requiredGoalType;
    public int GetGoalsRequired() => _goalsRequired;
    public int GetGoalsCompleted() => _goalsCompleted;
    public int GetGoldReward() => _goldReward;
    public int GetXpReward() => _xpReward;
    public bool IsDefeated() => _currentHealth <= 0;
    public bool IsGoalRequirementMet() => _goalsCompleted >= _goalsRequired;

    public void RecordGoalProgress() => _goalsCompleted = Math.Min(_goalsCompleted + 1, _goalsRequired);

    public void ApplyHourlyDamage(int villagePower, int libraryBypassBonus)
    {
        if (!IsGoalRequirementMet()) return;
        int damage = Math.Max(1, villagePower - Math.Max(0, _damageResistance - libraryBypassBonus));
        _currentHealth = Math.Max(0, _currentHealth - damage);
    }

    public string GetStatusString()
    {
        int bars = (int)(10.0 * _currentHealth / _maxHealth);
        string healthBar = $"[{new string('#', bars)}{new string('-', 10 - bars)}]";
        string locked = IsGoalRequirementMet() ? "" : " [LOCKED - complete required goals first]";
        return $"  {_name}\n  HP: {healthBar} {_currentHealth}/{_maxHealth}" +
               $"\n  Goals needed: {_goalsCompleted}/{_goalsRequired} {_requiredGoalType}{locked}" +
               $"\n  Reward on defeat: +{_xpReward} XP, +{_goldReward} gold";
    }

    public string Encode() =>
        $"{_name}|{_maxHealth}|{_currentHealth}|{_requiredGoalType}|{_goalsRequired}|{_goalsCompleted}|{_damageResistance}|{_goldReward}|{_xpReward}";
}
