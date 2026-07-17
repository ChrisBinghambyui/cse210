namespace ScrollsAndSteel
{
    public class Nordal : Race
    {
        public Nordal() : base(
            "Nordal",
            "The Nordal come from the northern reaches: tundra, glacier-carved fjords, and seas that freeze in winter. Large, hardy, shaped by a culture that prizes strength, directness, and communal endurance. Most Nordal magic users are unusual among their own people, though not unwelcome; the north has its own traditions of rune-working and storm-calling that most southerners don't recognise as the same discipline.")
        {
        }

        public override void ApplyRacialBonuses(Character character)
        {
            character.GetAttribute("STR").ApplyModifier(10);
            character.GetAttribute("END").ApplyModifier(5);
            character.GetAttribute("INT").ApplyModifier(-5);
            character.GetAttribute("WIL").ApplyModifier(-5);
            character.GetAttribute("PER").ApplyModifier(-5);
        }

        public override void ApplySkillBonuses(Character character)
        {
            string weapon = ChooseOneOfTwo("Nordal: choose a weapon skill to gain +10.", "Blunt Weapon", "Long Blade");
            character.GetSkill(weapon).IncreaseSkill(10);

            foreach (string school in SkillDefinitions.MagicSchools)
            {
                character.GetSkill(school).SetValue(5);
            }
        }

        public override string GetBonusSummary()
        {
            return "+10 STR, +5 END, -5 INT, -5 WIL, -5 PER\n+10 Blunt Weapon or Long Blade (choose). All magic schools start at 5 regardless of skill tier.";
        }
    }
}
