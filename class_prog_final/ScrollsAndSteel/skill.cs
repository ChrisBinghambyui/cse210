using System;

namespace ScrollsAndSteel
{
    // Represents a single trained Skill (Long Blade, Sneak, Alchemy, etc.).
    // A skill's value can never exceed the average of its governing Attribute(s) -
    // see Chapter 3 of the rulebook.
    public class Skill
    {
        private string _name;
        private int _value;
        private Attribute[] _governingAttributes;

        public Skill(string name, int value, Attribute[] governingAttributes)
        {
            _name = name;
            _governingAttributes = governingAttributes;
            _value = Math.Min(value, GetCap());
        }

        public string GetName()
        {
            return _name;
        }

        public int GetValue()
        {
            return _value;
        }

        public Attribute[] GetGoverningAttributes()
        {
            return _governingAttributes;
        }

        // The highest this skill may currently reach: the average of its
        // governing Attribute(s), rounded down.
        public int GetCap()
        {
            int sum = 0;
            foreach (Attribute attribute in _governingAttributes)
            {
                sum += attribute.GetValue();
            }
            return sum / _governingAttributes.Length;
        }

        public void IncreaseSkill(int amount)
        {
            int cap = GetCap();
            _value = Math.Min(_value + amount, cap);
        }
    }
}
