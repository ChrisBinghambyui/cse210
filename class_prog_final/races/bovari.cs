namespace ScrollsAndSteel
{
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
