namespace ScrollsAndSteel
{
    // Chapter 12: Sunblade - the Wandrkin. Humans devoted to swordsmanship as one of the highest arts.
    public class Sunblade : Race
    {
        public Sunblade() : base(
            "Sunblade",
            "The Wandrkin: Humans devoted to swordsmanship as one of the highest arts.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }
    }
}
