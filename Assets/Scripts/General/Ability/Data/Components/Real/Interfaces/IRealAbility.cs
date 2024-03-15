namespace AutoChessRPG
{
    public interface IRealAbility
    {
        public bool OnLevelUp();

        public RealPowerPacket GetPowerPacket();

    }
}
