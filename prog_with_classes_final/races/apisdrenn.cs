namespace ScrollsAndSteel
{
    public class Apisdrenn : Race
    {
        public Apisdrenn() : base(
            "Apisdrenn",
            "The Apisdrenn are the most widely encountered dwarven people, builders, miners, and smiths who maintain a vast network of mountain strongholds and underground trade routes. Stocky even by dwarven standards, with a well-earned reputation for stubbornness and craftsmanship that they consider deeply accurate. Their ales are the best in the known world, and the only thing that can compare to their craftsmanship and reputation is their generosity.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(10);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Heavy Armor").IncreaseSkill(10);

            string weapon = ChooseOneOfTwo("Apisdrenn: choose a weapon skill to gain +10.", "Blunt Weapon", "Axe");
            character.GetSkill(weapon).IncreaseSkill(10);
        }

        public override string GetBonusSummary()
        {
            return "+10 END, +5 STR, -5 SPD, -5 AGI, -5 PER\n+10 Heavy Armor, +10 Blunt Weapon or Axe (choose)";
        }
    }
}
