using System;
using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class Character
    {
        private string _name;
        private Race _race;
        private int _level;
        private Dictionary<string, Attribute> _attributes;
        private List<Skill> _skills;

        private int _maxHP;
        private int _currentHP;
        private int _maxMP;
        private int _currentMP;
        private int _maxFP;
        private int _currentFP;

        public Character(string name, Race race, Dictionary<string, int> startingAttributes)
            : this(name, race, startingAttributes, true)
        {
        }

        public Character(string name, Race race, Dictionary<string, int> attributeValues, bool applyRacialBonuses)
        {
            _name = name;
            _race = race;
            _level = 1;
            _skills = new List<Skill>();

            _attributes = new Dictionary<string, Attribute>();
            foreach (KeyValuePair<string, int> entry in attributeValues)
            {
                _attributes[entry.Key] = new Attribute(entry.Key, entry.Value);
            }

            if (applyRacialBonuses)
            {
                ApplyRace();
            }
        }

        private void ApplyRace()
        {
            _race.ApplyRacialBonuses(this);
        }

        public string GetName()
        {
            return _name;
        }

        public Race GetRace()
        {
            return _race;
        }

        public int GetLevel()
        {
            return _level;
        }

        public Attribute GetAttribute(string name)
        {
            return _attributes[name];
        }

        public void AddSkill(Skill skill)
        {
            _skills.Add(skill);
        }

        public Skill GetSkill(string name)
        {
            foreach (Skill skill in _skills)
            {
                if (skill.GetName() == name)
                {
                    return skill;
                }
            }
            return null;
        }

        public List<Skill> GetAllSkills()
        {
            return _skills;
        }

        public string Encode()
        {
            string[] attributeOrder = { "STR", "END", "AGI", "SPD", "INT", "WIL", "PER", "LCK" };

            string attributeText = "";
            for (int i = 0; i < attributeOrder.Length; i++)
            {
                if (i > 0)
                {
                    attributeText = attributeText + ",";
                }
                string attributeName = attributeOrder[i];
                int attributeValue = GetAttribute(attributeName).GetValue();
                attributeText = attributeText + attributeName + ":" + attributeValue;
            }

            string skillText = "";
            for (int i = 0; i < _skills.Count; i++)
            {
                if (i > 0)
                {
                    skillText = skillText + ",";
                }
                skillText = skillText + _skills[i].GetName() + ":" + _skills[i].GetValue();
            }

            return _name + "|" + _race.GetName() + "|" + attributeText + "|" + skillText;
        }

        public void CalculateDerivedStats()
        {
            int end = GetAttribute("END").GetValue();
            int intel = GetAttribute("INT").GetValue();
            int str = GetAttribute("STR").GetValue();
            int agi = GetAttribute("AGI").GetValue();
            int spd = GetAttribute("SPD").GetValue();

            _maxHP = (end / 5) + (2 * (_level - 1));
            _maxMP = (intel / 5) + (1 * (_level - 1));
            _maxFP = (end + str + agi + spd) / 10;

            _currentHP = _maxHP;
            _currentMP = _maxMP;
            _currentFP = _maxFP;
        }

        public int GetInitiativeBonus()
        {
            return GetAttribute("AGI").GetValue() / 10;
        }

        public int GetLuckBonus()
        {
            return GetAttribute("LCK").GetValue() / 10;
        }

        public int GetMeleeDamageBonus()
        {
            return GetAttribute("STR").GetValue() / 20;
        }

        public int GetAgilityDamageBonus()
        {
            return GetAttribute("AGI").GetValue() / 20;
        }

        public int GetMovementSpeed()
        {
            int raw = GetAttribute("SPD").GetValue() / 2;
            int roundedToFive = (int)(Math.Round(raw / 5.0) * 5);
            return Math.Max(roundedToFive, 10);
        }

        public int GetCarryWeight()
        {
            return Math.Max(GetAttribute("STR").GetValue() * 5, 50);
        }

        public int GetMaxHP()
        {
            return _maxHP;
        }

        public int GetCurrentHP()
        {
            return _currentHP;
        }

        public int GetMaxMP()
        {
            return _maxMP;
        }

        public int GetCurrentMP()
        {
            return _currentMP;
        }

        public int GetMaxFP()
        {
            return _maxFP;
        }

        public int GetCurrentFP()
        {
            return _currentFP;
        }

        public void TakeDamage(int amount)
        {
            _currentHP = Math.Max(_currentHP - amount, 0);
        }

        public void Heal(int amount)
        {
            _currentHP = Math.Min(_currentHP + amount, _maxHP);
        }

        public void SpendFatigue(int amount)
        {
            _currentFP = Math.Max(_currentFP - amount, 0);
        }

        public void RecoverFatigue(int amount)
        {
            _currentFP = Math.Min(_currentFP + amount, _maxFP);
        }
    }
}
