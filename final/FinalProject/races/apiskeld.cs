namespace ScrollsAndSteel
{
    public class Apiskeld : Race
    {
        public Apiskeld() : base(
            "Apiskeld",
            "The Apiskeld emerge from deeper and older holds than their Apisdrenn kin. Their connection to heat and geological pressure runs almost spiritual, their skin carries a faint metallic sheen in firelight, and many Apiskeld describe the sound of a working forge as something close to music. Apiskeld smiths and enchanters are regarded as among the finest practitioners in the known world, and they charge accordingly.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Enchanting").IncreaseSkill(10);
            character.GetSkill("Blunt Weapon").IncreaseSkill(5);
            character.GetSkill("Heavy Armor").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 END, +5 STR, +5 INT, -5 SPD, -5 AGI, -5 PER\n+10 Enchanting, +5 Blunt Weapon, +5 Heavy Armor";
        }
    }
}
