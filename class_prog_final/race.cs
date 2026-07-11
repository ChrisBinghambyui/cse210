namespace ScrollsAndSteel
{
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

        public abstract void ApplyRacialBonuses(Character character);
    }
}
