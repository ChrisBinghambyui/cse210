namespace ScrollsAndSteel
{
    public class Veildrift : Race
    {
        public Veildrift() : base(
            "Veildrift",
            "The Hauntborn: A pale, ghostly people of unknown origin.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("WIL").ApplyModifier(10);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
