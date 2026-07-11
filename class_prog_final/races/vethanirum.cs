namespace ScrollsAndSteel
{
    public class Vethanirum : Race
    {
        public Vethanirum() : base(
            "Vethanirum",
            "The Borderborn: Elves of the cultural fringes, practical and diverse.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("PER").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
        }
    }
}
