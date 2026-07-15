using System.Collections.Generic;

namespace ScrollsAndSteel
{
    public class Gorirum : Race
    {
        public Gorirum() : base(
            "Gorirum",
            "The Gorirum insist, and their oldest scholars can produce genealogical records that are at minimum inconclusive, that their people and the elven peoples share a common ancestor, diverged in some ancient age before written history. Neither the faintly longer ears, the slightly higher natural aptitude for magic, nor the longer-than-average lifespan they carry compared to other orcish lineages are accidental, as far as they're concerned.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            List<string> schools = ChooseManyFromList("Gorirum: choose two magic schools to each gain +5.", SkillDefinitions.MagicSchools, 2);
            foreach (string school in schools)
            {
                character.GetSkill(school).IncreaseSkill(5);
            }

            character.GetSkill("Long Blade").IncreaseSkill(10);
        }

        public override string GetBonusSummary()
        {
            return "+5 STR, +5 INT, +5 WIL, -5 AGI, -5 PER, -5 LCK\n+5 to two magic schools (choose), +10 Long Blade";
        }
    }
}
