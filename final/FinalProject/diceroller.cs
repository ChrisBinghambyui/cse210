using System;

namespace ScrollsAndSteel
{
    public class DiceRoller
    {
        private Random _random;

        public DiceRoller()
        {
            _random = new Random();
        }

        public int RollD100()
        {
            return _random.Next(1, 101);
        }

        public int RollWithAdvantage()
        {
            int first = RollD100();
            int second = RollD100();
            return Math.Min(first, second);
        }

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
