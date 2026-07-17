namespace ScrollsAndSteel
{
    public class Ashirum : Race
    {
        public Ashirum() : base(
            "Ashirum",
            "The Ashirum have lived for centuries around active volcanic regions, their culture shaped by constant fire and geological instability. Their dark complexions are marked by subtle ember-coloured eyes and faint natural patterns on the skin. Ashirum society emphasises resilience, self-sufficiency, and the understanding that comfort is temporary but adaptation is permanent.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            string school = ChooseOneFromList("Ashirum: choose one magic school to gain +10.", SkillDefinitions.MagicSchools);
            character.GetSkill(school).IncreaseSkill(10);

            string blade = ChooseOneOfTwo("Ashirum: choose a blade skill to gain +5.", "Long Blade", "Short Blade");
            character.GetSkill(blade).IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 INT, +5 AGI, +5 SPD, -5 END, -5 PER, -5 LCK\n+10 to one magic school, +5 Long Blade or Short Blade (choose)";
        }
    }
}
