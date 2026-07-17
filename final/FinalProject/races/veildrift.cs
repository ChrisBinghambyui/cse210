namespace ScrollsAndSteel
{
    public class Veildrift : Race
    {
        public Veildrift() : base(
            "Veildrift",
            "No one knows where the Veildrift came from. The oldest records describe them appearing at the edges of battlefields, in the ruins of collapsed towers, and at sites where large amounts of magic were spent in catastrophic ways. Current scholars debate whether they are the descendants of people who survived a planar boundary event, or something stranger still. The Veildrift themselves rarely discuss it, either because they don't know or because they have decided it doesn't matter.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("WIL").ApplyModifier(10);
            character.GetAttribute("INT").ApplyModifier(5);
            character.GetAttribute("STR").ApplyModifier(-5);
            character.GetAttribute("END").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Divination").IncreaseSkill(10);
            character.GetSkill("Sneak").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+10 WIL, +5 INT, -5 STR, -5 END, -5 PER\n+10 Divination, +5 Sneak";
        }
    }
}
