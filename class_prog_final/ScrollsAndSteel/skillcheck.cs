namespace ScrollsAndSteel
{
    // Resolves one skill check per Appendix F: "ROLL d100 equal to or under
    // (Skill + Luck Bonus). Difficulty adds to your roll." A positive
    // difficulty makes the check harder; a negative difficulty makes it easier.
    public class SkillCheck
    {
        private Character _character;
        private Skill _skill;
        private int _difficulty;
        private DiceRoller _diceRoller;

        public SkillCheck(Character character, Skill skill, int difficulty)
        {
            _character = character;
            _skill = skill;
            _difficulty = difficulty;
            _diceRoller = new DiceRoller();
        }

        public string PerformCheck()
        {
            int naturalRoll = _diceRoller.RollD100();
            string critical = _diceRoller.CheckCritical(naturalRoll);

            if (critical == "CriticalSuccess")
            {
                return "Natural " + naturalRoll + " - CRITICAL SUCCESS!";
            }
            if (critical == "CriticalFailure")
            {
                return "Natural " + naturalRoll + " - CRITICAL FAILURE!";
            }

            int target = _skill.GetValue() + _character.GetLuckBonus();
            int adjustedRoll = naturalRoll + _difficulty;

            return DetermineOutcome(naturalRoll, adjustedRoll, target);
        }

        private string DetermineOutcome(int naturalRoll, int adjustedRoll, int target)
        {
            string difficultyText;
            if (_difficulty > 0)
            {
                difficultyText = "+" + _difficulty;
            }
            else
            {
                difficultyText = _difficulty.ToString();
            }

            string rollSummary = "Rolled " + naturalRoll + ", difficulty " + difficultyText +
                " = " + adjustedRoll + " vs target " + target;

            if (adjustedRoll <= target)
            {
                return rollSummary + " - Success!";
            }
            else
            {
                return rollSummary + " - Failure.";
            }
        }
    }
}
