namespace ScrollsAndSteel
{
    public class Sylvarirum : Race
    {
        public Sylvarirum() : base(
            "Sylvarirum",
            "The Greenwood Folk: Small-framed, quick forest dwellers at home in the wild.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("AGI").ApplyModifier(10);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-10);
            character.GetAttribute("END").ApplyModifier(-5);
        }
    }
}
