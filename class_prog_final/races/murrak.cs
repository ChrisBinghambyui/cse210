namespace ScrollsAndSteel
{
    public class Murrak : Race
    {
        public Murrak() : base(
            "Murrak",
            "The Ashblood: A people of disputed origin with an instinctive resonance with magic.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-10);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
