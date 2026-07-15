namespace ScrollsAndSteel
{
    public class Imperials : Race
    {
        public Imperials() : base(
            "Imperials",
            "Imperials are the most numerous people in the lowland cities and trade routes. Their civilization is organized, mercantile, and built on the conviction that good administration produces better outcomes than raw power. They produce diplomats, merchants, soldiers, and bureaucrats in equal measure, and their language and coinage have become the continental standard through quiet, persistent usefulness rather than conquest.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("PER").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("LCK").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("AGI").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Speechcraft").IncreaseSkill(10);
            character.GetSkill("Mercantile").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 PER, +5 END, +5 LCK, -5 INT, -5 AGI, -5 WIL\n+10 Speechcraft, +5 Mercantile";
        }
    }
}
