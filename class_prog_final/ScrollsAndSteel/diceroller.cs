using System;

namespace ScrollsAndSteel
{
    // Handles all d100 rolling per the Quick Reference in Appendix F:
    // roll equal to or under your target to succeed, natural 1-2 always
    // succeeds (critical), natural 98-99 always fails (critical).
    public class DiceRoller
    {
        private Random _random;

        public DiceRoller()
        {
            _random = new Random();
        }

        public int RollD100()
        {
            return _random.Next(1, 101); // 1-100 inclusive
        }

        // Advantage: roll twice, take the lower (better) result.
        public int RollWithAdvantage()
        {
            int first = RollD100();
            int second = RollD100();
            return Math.Min(first, second);
        }

        // Disadvantage: roll twice, take the higher (worse) result.
        public int RollWithDisadvantage()
        {
            int first = RollD100();
            int second = RollD100();
            return Math.Max(first, second);
        }

        public string CheckCritical(int naturalRoll)
        {
            if (naturalRoll <= 2) return "CriticalSuccess";
            if (naturalRoll >= 98) return "CriticalFailure";
            return "None";
        }
    }
}
