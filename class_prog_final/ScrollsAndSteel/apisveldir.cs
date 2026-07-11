namespace ScrollsAndSteel
{
    // Chapter 12: Apisveldir - the Gem Seekers. Rare dwarven lineage fixated on gemstones and their history.
    public class Apisveldir : Race
    {
        public Apisveldir() : base(
            "Apisveldir",
            "The Gem Seekers: Rare dwarven lineage fixated on gemstones and their history.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("PER").ApplyModifier(5);
            character.GetAttribute("LCK").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
        }
    }
}
