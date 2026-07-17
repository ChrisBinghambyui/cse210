namespace ScrollsAndSteel
{
    public class Vethanirum : Race
    {
        public Vethanirum() : base(
            "Vethanirum",
            "The Vethanirum emerged generations ago at the cultural fringes of elven culture and today form their own distinct people. They carry natural attunement to magic without Solarirum-level immersion, and a practicality that comes from navigating between worlds their whole lives. Most describe themselves simply as 'practical', which is the polite word for someone who has learned not to take anyone's traditions too seriously, including their own.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("PER").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            foreach (string school in SkillDefinitions.MagicSchools)
            {
                character.GetSkill(school).IncreaseSkill(5);
            }
        }

        public override string GetBonusSummary()
        {
            return "+5 INT, +5 WIL, +5 PER, -5 STR, -5 END, -5 AGI\n+5 to all magic schools at character creation";
        }
    }
}
