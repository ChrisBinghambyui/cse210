namespace ScrollsAndSteel
{
    public class Sunblade : Race
    {
        public Sunblade() : base(
            "Sunblade",
            "The Sunblade come from a culture that treats swordsmanship as one of the highest arts. Their history is one of migration, resistance, and the cultivation of martial excellence as both survival tool and cultural identity. Sunblade tradition often leads individuals to travel far from their homelands, some as merchants, some as mercenaries, some as scholars.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("LCK").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Long Blade").IncreaseSkill(10);
            character.GetSkill("Block").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 STR, +5 AGI, -5 INT, -5 WIL, -5 LCK\n+10 Long Blade, +5 Block";
        }
    }
}
