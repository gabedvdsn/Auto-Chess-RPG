namespace AutoChessRPG
{
    public enum Affiliation
    {
        Ally,
        Enemy,
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
                _ => Affiliation.NONE
            };
        }
    }
}