namespace ScrollsAndSteel
{
    // Chapter 12: Imperials - the Hearthborn. The most numerous human people, organized and mercantile.
    public class Imperials : Race
    {
        public Imperials() : base(
            "Imperials",
            "The Hearthborn: The most numerous human people, organized and mercantile.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("PER").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("LCK").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
        }
    }
}
