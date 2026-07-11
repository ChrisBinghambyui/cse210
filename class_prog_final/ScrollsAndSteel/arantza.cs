namespace ScrollsAndSteel
{
    // Chapter 12: Arantza - the Broodborn. Six-limbed, eight-eyed beastborn spider-folk.
    public class Arantza : Race
    {
        public Arantza() : base(
            "Arantza",
            "The Broodborn: Six-limbed, eight-eyed beastborn spider-folk.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
