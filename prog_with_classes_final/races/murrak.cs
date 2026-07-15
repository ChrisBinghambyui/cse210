namespace ScrollsAndSteel
{
    public class Murrak : Race
    {
        public Murrak() : base(
            "Murrak",
            "The Murrak are a people of disputed origin. Some scholars claim they are the surviving descendants of a civilization that bargained poorly with something ancient. Others suggest they are simply a human lineage that diverged under extreme magical saturation. The Murrak themselves do not spend much time on the question: they are here, they are capable, and the world's opinion of their origins is their own problem.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("WIL").ApplyModifier(5);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-10);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            string chosen = ChooseOneOfTwo("Murrak: choose a school to gain +10.", "Elementalism", "Illusion");
            character.GetSkill(chosen).IncreaseSkill(10);

            character.GetSkill("Enchanting").IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 WIL, +5 END, -10 INT, -5 PER\n+10 Elementalism or Illusion (choose), +5 Enchanting";
        }
    }
}
