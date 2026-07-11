namespace ScrollsAndSteel
{
    // Chapter 12: Nordal - the Frost-Kin. Large, hardy humans from the northern tundra.
    public class Nordal : Race
    {
        public Nordal() : base(
            "Nordal",
            "The Frost-Kin: Large, hardy humans from the northern tundra.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
