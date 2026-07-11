namespace ScrollsAndSteel
{
    // Chapter 12: Apiskeld - the Forge Lords. Dwarven smiths and enchanters with a spiritual bond to fire.
    public class Apiskeld : Race
    {
        public Apiskeld() : base(
            "Apiskeld",
            "The Forge Lords: Dwarven smiths and enchanters with a spiritual bond to fire.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }
    }
}
