namespace ScrollsAndSteel
{
    // Chapter 12: Ashveld - the Cindershorn Folk. Pragmatic humans from the volcanic highlands.
    public class Ashveld : Race
    {
        public Ashveld() : base(
            "Ashveld",
            "The Cindershorn Folk: Pragmatic humans from the volcanic highlands.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("LCK").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
