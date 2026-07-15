namespace ScrollsAndSteel
{
    public class Arantza : Race
    {
        public Arantza() : base(
            "Arantza",
            "The Arantza are bipedal spider-folk, primitive in the sense that their culture is oral, decentralized, and largely pre-agricultural, not in the sense of being simple. They are older than most of the cultures that would call them primitive, and their memory of the world's early shape is carried in stories that scholars have been trying to transcribe for a hundred years with limited success.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("SPD").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Athletics").IncreaseSkill(10);
            character.GetSkill("Sneak").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 AGI, +5 SPD, +5 STR, -5 INT, -5 WIL, -5 PER\n+10 Athletics (climbing), +5 Sneak";
        }
    }
}
