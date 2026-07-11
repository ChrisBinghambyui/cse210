namespace ScrollsAndSteel
{
    // Small helper that builds a Race object from its name. Used by Program
    // when creating a character and by CharacterFileManager when loading one,
    // so the list of races only has to be typed out in one place.
    public static class RaceFactory
    {
        public static Race CreateByName(string raceName)
        {
            switch (raceName)
            {
                case "Solarirum": return new Solarirum();
                case "Sylvarirum": return new Sylvarirum();
                case "Vethanirum": return new Vethanirum();
                case "Ashirum": return new Ashirum();
                case "Apisdrenn": return new Apisdrenn();
                case "Apiskeld": return new Apiskeld();
                case "Apisveldir": return new Apisveldir();
                case "Kreln": return new Kreln();
                case "Imperials": return new Imperials();
                case "Nordal": return new Nordal();
                case "Sunblade": return new Sunblade();
                case "Ashveld": return new Ashveld();
                case "Stoneguard": return new Stoneguard();
                case "Gorirum": return new Gorirum();
                case "Veildrift": return new Veildrift();
                case "Murrak": return new Murrak();
                case "Bovari": return new Bovari();
                case "Naukin": return new Naukin();
                case "Arantza": return new Arantza();
                default: return new Verdathi();
            }
        }
    }
}
