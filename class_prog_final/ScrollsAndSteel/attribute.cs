using System;

namespace ScrollsAndSteel
{
    // Represents a single core Attribute (STR, END, AGI, SPD, INT, WIL, PER, LCK).
    // Values are always kept between 10 and 100, per Chapter 2 of the rulebook.
    public class Attribute
    {
        private string _name;
        private int _value;

        private const int MinValue = 10;
        private const int MaxValue = 100;

        public Attribute(string name, int value)
        {
            _name = name;
            _value = Clamp(value);
        }

        public string GetName()
        {
            return _name;
        }

        public int GetValue()
        {
            return _value;
        }

        public void SetValue(int value)
        {
            _value = Clamp(value);
        }

        // Used for racial bonuses/penalties, which raise or lower an Attribute
        // by a fixed amount rather than setting it outright.
        public void ApplyModifier(int amount)
        {
            _value = Clamp(_value + amount);
        }

        private int Clamp(int value)
        {
            if (value < MinValue) return MinValue;
            if (value > MaxValue) return MaxValue;
            return value;
        }
    }
}
