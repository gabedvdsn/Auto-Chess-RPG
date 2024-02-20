namespace AutoChessRPG
{
    public enum Affiliation
    {
        Ally,
        Enemy,
        Neutral,
        NONE
    }

    public static class AffiliationManager
    {
        public static Affiliation GetOpposingAffiliation(Affiliation source)
        {
            return source switch
            {
                Affiliation.Ally => Affiliation.Enemy,
                Affiliation.Enemy => Affiliation.Ally,
                Affiliation.Neutral => Affiliation.NONE,
                _ => Affiliation.NONE
            };
        }
    }
}