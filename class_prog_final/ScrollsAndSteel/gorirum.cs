namespace ScrollsAndSteel
{
    // Chapter 12: Gorirum - the Old Kindred. Orcs who claim ancient kinship with the elven peoples.
    public class Gorirum : Race
    {
        public Gorirum() : base(
            "Gorirum",
            "The Old Kindred: Orcs who claim ancient kinship with the elven peoples.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }
    }
}
