using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class Bovari : Race
    {
        public Bovari() : base(
            "Bovari",
            "The Bovari are a large and immediately distinctive people: broadly built at the shoulder, standing just under six feet in most cases, with the squared heads and prominent curved horns of bovine ancestry. Both males and females bear horns, and Bovari culture attaches enormous personal significance to them, horns are cultivated, carved, painted, and adorned across a lifetime, and their condition and decoration communicate more about a Bovari's personal history than any word of introduction.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("INT").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Athletics").IncreaseSkill(10);

            List<string> chosen = ChooseManyFromList("Bovari: choose two non-combat skills to each gain +5.", SkillDefinitions.NonCombatSkills, 2);
            foreach (string skillName in chosen)
            {
                character.GetSkill(skillName).IncreaseSkill(5);
            }
        }

        public override string GetBonusSummary()
        {
            return "+10 STR, +5 END, -5 AGI, -5 SPD, -5 INT\n+10 Athletics, +5 to any two non-combat skills (choose at character creation)";
        }
    }
}
