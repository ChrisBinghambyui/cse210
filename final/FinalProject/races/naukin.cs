namespace ScrollsAndSteel
{
    public class Naukin : Race
    {
        public Naukin() : base(
            "Naukin",
            "The Naukin are ratfolk: lean, quick, whisker-faced, and collectively the product of centuries of exploitation at the hands of mage-scholars who used them as subjects, test cases, servants, and occasionally ingredients. Their escape from the great wizard academies is a matter of historical record; how they organized it is not, because the Naukin have never discussed it with outsiders.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("AGI").ApplyModifier(10);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Sneak").IncreaseSkill(10);
            character.GetSkill("Security").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 AGI, +5 SPD, -5 STR, -5 END, -5 PER\n+10 Sneak, +5 Security";
        }
    }
}
