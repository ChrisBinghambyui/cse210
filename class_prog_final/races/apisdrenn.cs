namespace ScrollsAndSteel
{
    public class Apisdrenn : Race
    {
        public Apisdrenn() : base(
            "Apisdrenn",
            "The Mountain Kin: Stocky dwarven builders, miners, and smiths.")
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
    }
}
