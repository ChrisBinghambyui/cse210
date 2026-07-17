namespace ScrollsAndSteel
{
    public class Verdathi : Race
    {
        public Verdathi() : base(
            "Verdathi",
            "The Verdathi are a reptilian people from the vast marshland territories of the southern coast, reliable in ways that are hard to fake: the swamp does not forgive pretense. On land, Verdathi are capable, solid, if slightly slower than average. In water, they are something else entirely, faster than almost anything that did not evolve there, with capabilities land-dwellers simply cannot counter.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Athletics").IncreaseSkill(10);
            character.GetSkill("Security").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 END, +5 AGI, +5 INT, -5 STR, -5 PER, -5 LCK\n+10 Athletics, +5 Security";
        }
    }
}
