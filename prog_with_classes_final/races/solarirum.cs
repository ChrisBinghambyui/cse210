namespace ScrollsAndSteel
{
    public class Solarirum : Race
    {
        public Solarirum() : base(
            "Solarirum",
            "The Solarirum trace their lineage to the oldest elven civilization, a culture of scholar-casters that shaped the continent's earliest institutions. Tall, angular, and faintly luminescent in direct sunlight, most live in layered city-states where magical study is as ordinary as farming is elsewhere. They regard themselves as custodians of knowledge, a belief their neighbors find either admirable or insufferable, depending on how recently they've had to deal with one.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("INT").ApplyModifier(10);
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("SPD").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            string school = ChooseOneFromList("Solarirum: choose one magic school to gain +10.", SkillDefinitions.MagicSchools);
            character.GetSkill(school).IncreaseSkill(10);
        }

        public override string GetBonusSummary()
        {
            return "+10 INT, +5 WIL, -5 END, -5 STR, -5 SPD\n+10 to one chosen magic school at character creation";
        }
    }
}
