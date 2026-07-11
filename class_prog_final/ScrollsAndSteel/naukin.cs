namespace ScrollsAndSteel
{
    // Chapter 12: Naukin - the Ratfolk. Quick, whisker-faced beastborn, deeply wary of magic.
    public class Naukin : Race
    {
        public Naukin() : base(
            "Naukin",
            "The Ratfolk: Quick, whisker-faced beastborn, deeply wary of magic.")
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
    }
}
