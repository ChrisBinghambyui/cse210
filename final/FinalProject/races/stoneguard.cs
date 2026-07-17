namespace ScrollsAndSteel
{
    public class Stoneguard : Race
    {
        public Stoneguard() : base(
            "Stoneguard",
            "Stoneguard culture is organized around the war-band, the smith, and the concept of earned authority. They are direct, pragmatic, and deeply invested in the idea that capability matters more than birth. Their settlements are fortified and self-sufficient, built for war not because they seek it, but because they have learned to expect it.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            string weapon = ChooseOneOfTwo("Stoneguard: choose a weapon skill to gain +10.", "Blunt Weapon", "Axe");
            character.GetSkill(weapon).IncreaseSkill(10);

            character.GetSkill("Heavy Armor").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 STR, +5 END, -5 PER, -5 LCK, -5 SPD\n+10 Blunt Weapon or Axe (choose), +5 Heavy Armor";
        }
    }
}
