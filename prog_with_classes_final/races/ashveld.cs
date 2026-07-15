namespace ScrollsAndSteel
{
    public class Ashveld : Race
    {
        public Ashveld() : base(
            "Ashveld",
            "The Ashveld are a human lineage from the volcanic highlands at the continent's interior, where the earth is young and the air tastes of sulfur. They are not defined by fire magic, a stereotype that irritates them, but by the practical, unsentimental culture that emerges from living where the ground occasionally kills you. The Ashveld are adaptable, direct, and deeply pragmatic, with a cultural tradition of improvised problem-solving that makes them valued in diverse expeditions.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("AGI").ApplyModifier(5);
            character.GetAttribute("LCK").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            character.GetSkill("Athletics").IncreaseSkill(10);

            string[] options = new string[] { "Alchemy", "Armorer", "Security" };
            string chosen = ChooseOneFromList("Ashveld: choose a crafting or survival skill to gain +5.", options);
            character.GetSkill(chosen).IncreaseSkill(5);
        }

        public override string GetBonusSummary()
        {
            return "+5 END, +5 AGI, +5 LCK, -5 INT, -5 WIL, -5 PER\n+10 Athletics, +5 to one crafting or survival skill (Alchemy, Armorer, or Security)";
        }
    }
}
