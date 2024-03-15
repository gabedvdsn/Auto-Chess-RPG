namespace AutoChessRPG
{
    public interface ICreatesRealProjectile : IRealAbility
    {
        public void OnProjectileHit(EncounterAutoCharacterController controller);
    }
}
