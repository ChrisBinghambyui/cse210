namespace ScrollsAndSteel
{
    // Chapter 12: Ashirum - the Ember People. +5 INT, +5 AGI, +5 SPD, -5 END, -5 PER, -5 LCK.
    public class Ashirum : Race
    {
        public Ashirum() : base(
            "Ashirum",
            "The Ember People: fire-touched elves of the volcanic heartlands.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }
    }
}
