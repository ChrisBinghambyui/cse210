namespace ScrollsAndSteel
{
    // Parent class for every playable lineage. Chapter 12 of the rulebook lists
    // twelve peoples; each one becomes its own subclass that overrides
    // ApplyRacialBonuses with its specific Attribute bonuses and penalties.
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

        // Each lineage overrides this to apply its own Attribute bonuses
        // and penalties to a freshly created Character.
        public abstract void ApplyRacialBonuses(Character character);
    }
}
