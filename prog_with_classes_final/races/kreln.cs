namespace ScrollsAndSteel
{
    public class Kreln : Race
    {
        public Kreln() : base(
            "Kreln",
            "The Deepstone Kin: Dwarven descendants of a civilization that retreated into the deep ocean.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(10);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }
    }
}
