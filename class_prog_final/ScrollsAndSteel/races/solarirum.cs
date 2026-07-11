namespace ScrollsAndSteel
{
    public class Solarirum : Race
    {
        public Solarirum() : base(
            "Solarirum",
            "The Gilt-Born: Elven scholar-casters, tall and faintly luminescent.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(10);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
        }
    }
}
