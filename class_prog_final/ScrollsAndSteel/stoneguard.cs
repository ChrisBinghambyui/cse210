namespace ScrollsAndSteel
{
    // Chapter 12: Stoneguard - the Iron-Blooded. Orcish war-band culture built on earned authority.
    public class Stoneguard : Race
    {
        public Stoneguard() : base(
            "Stoneguard",
            "The Iron-Blooded: Orcish war-band culture built on earned authority.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
        }
    }
}
