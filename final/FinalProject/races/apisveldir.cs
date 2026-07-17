namespace ScrollsAndSteel
{
    public class Apisveldir : Race
    {
        public Apisveldir() : base(
            "Apisveldir",
            "The Apisveldir are the rarest and most widely misunderstood of the dwarven peoples. To outsiders their fixation on rare gemstones looks like greed, they will divert an entire expedition to investigate a glimmer in a far tunnel, spend a month's wages on a single flawless sapphire, and turn down gold in favour of a fragment of something rarer. To the Apisveldir themselves, this is not acquisitiveness. It is closer to recognition.")
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

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Mercantile").IncreaseSkill(10);
            character.GetSkill("Enchanting").IncreaseSkill(10);
            character.GetSkill("Security").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 INT, +5 PER, +5 LCK, -5 STR, -5 END, -5 AGI\n+10 Mercantile, +10 Enchanting, +5 Security";
        }
    }
}
