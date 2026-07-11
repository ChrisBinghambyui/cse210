namespace ScrollsAndSteel
{
    public class Verdathi : Race
    {
        public Verdathi() : base(
            "Verdathi",
            "The Marsh-Born: Reptilian beastborn from the southern marshlands, at home in water.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }
    }
}
