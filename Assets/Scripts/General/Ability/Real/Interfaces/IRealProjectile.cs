namespace AutoChessRPG
{
    public interface IRealProjectile : IRealAbility
    {
        public void OnProjectileHit(EncounterAutoCharacterController controller);
    }
}
