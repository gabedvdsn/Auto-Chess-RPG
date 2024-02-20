namespace AutoChessRPG
{
    public enum Dispell
    {
        Weak = 0,
        Strong = 1
    }

    public static class DispellManager
    {
        public static bool DispellIsEffective(Dispell dispell, Dispell dispellRequirement) => dispell >= dispellRequirement;
    }
}