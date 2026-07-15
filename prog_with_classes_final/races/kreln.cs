namespace ScrollsAndSteel
{
    public class Kreln : Race
    {
        public Kreln() : base(
            "Kreln",
            "The Kreln are the remnants of a dwarven civilization that, long before recorded history, retreated entirely into the deep ocean. No one knows whether it was catastrophe, choice, or something stranger that drove them below the surface. What remained emerged over millennia as something distinctly its own: stocky and dense like their landbound cousins, but shaped by crushing pressure and lightless depths into a form that unsettles those encountering them for the first time.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(10);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Athletics").IncreaseSkill(10);

            string chosen = ChooseOneOfTwo("Kreln: choose a skill to gain +5.", "Heavy Armor", "Blunt Weapon");
            character.GetSkill(chosen).IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 END, +5 STR, -5 SPD, -5 PER, -5 LCK\n+10 Athletics (swimming), +5 Heavy Armor or Blunt Weapon (choose)";
        }
    }
}
