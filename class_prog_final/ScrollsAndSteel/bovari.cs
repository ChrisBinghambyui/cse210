namespace ScrollsAndSteel
{
    // Chapter 12: Bovari - the Ox-folk. +10 STR, +5 END, -5 AGI, -5 SPD, -5 INT.
    public class Bovari : Race
    {
        public Bovari() : base(
            "Bovari",
            "The Ox-folk: broad-shouldered, four-armed, and good-natured.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("INT").ApplyModifier(-5);
        }
    }
}
