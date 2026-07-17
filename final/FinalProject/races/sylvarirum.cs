namespace ScrollsAndSteel
{
    public class Sylvarirum : Race
    {
        public Sylvarirum() : base(
            "Sylvarirum",
            "Sylvarirum communities nestle in ancient forests in treehouse settlements invisible to the uninvited. Smaller-framed than most races but extraordinarily quick, they are at home in natural terrain that slows everyone else. Many spend years on solitary journeys, called the Wandering, before returning to their communities, treating it as both a coming-of-age and a spiritual discipline.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("AGI").ApplyModifier(10);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-10);
            character.GetAttribute("END").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Marksman").IncreaseSkill(10);
            character.GetSkill("Sneak").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 AGI, +5 SPD, -10 STR, -5 END\n+10 Marksman, +5 Sneak";
        }
    }
}
