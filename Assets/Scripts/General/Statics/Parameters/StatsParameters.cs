namespace AutoChessRPG
{
    public static class StatsParameters
    {
        // Base
        public const float BASE_ATTACK_SPEED = 100f;
        public const float BASE_ATTACK_TIME = 1.5f;

        public const float BASE_MOVESPEED = 2.5f;
        public const float BASE_ROTATION_SPEED = BASE_MOVESPEED + .75f;
        
        public const float BASE_MAGIC_RESIST = .2f;
        public const float BASE_PHYSICAL_RESIST = .2f;
        public const float BASE_DEFUFF_RESIST = .15f;
        
        // Min
        public const float MIN_MAX_HEALTH = 1f;
        public const float MIN_MAX_MANA = 0f;
        public const float MIN_ATTACK_DAMAGE = 0f;
        public const float MIN_ATTACK_SPEED = BASE_ATTACK_SPEED / (100 * BASE_ATTACK_TIME);
        public const float MIN_MOVESPEED = BASE_MOVESPEED;
        public const float MIN_ARMOR = 0f;
        public const float MIN_NEGATION = 0f;
        public const float MIN_PHYS_RESIST = BASE_MAGIC_RESIST;
        public const float MIN_MAG_RESIST = BASE_PHYSICAL_RESIST;
        public const float MIN_DEBUFF_RESIST = BASE_DEFUFF_RESIST;
        public const float MIN_HEALTH_REGEN = 0f;
        public const float MIN_MANA_REGEN = 0f;

        public const float MIN_ROTATION_SPEED = .33f;

        // Max
        public const float MAX_MAX_HEALTH = 9999f;
        public const float MAX_MAX_MANA = 9999f;
        public const float MAX_ATTACK_DAMAGE = 9999f;
        public const float MAX_ATTACK_SPEED = 5f;
        public const float MAX_MOVESPEED = 5f;
        public const float MAX_ARMOR = 9999f;
        public const float MAX_NEGATION = 9999f;
        public const float MAX_PHYS_RESIST = 1f;
        public const float MAX_MAG_RESIST = 1f;
        public const float MAX_DEBUFF_RESIST = 1f;
        public const float MAX_HEALTH_REGEN = 9999f;
        public const float MAX_MANA_REGEN = 9999f;

        public const float MAX_ROTATION_SPEED = 2.5f;
        
        // In game units to real
        public const float GAME_MOVESPEED_TO_REAL = 1 / 100f;
        public const float GAME_ATTACK_SPEED_TO_REAL = 1 / 100f;

        public const float GAME_PERCENT_TO_REAL = 1 / 100f;
        
        // Real units to in game units
        public const float REAL_MOVESPEED_TO_GAME = 1 / GAME_MOVESPEED_TO_REAL;
        public const float REAL_ATTACK_SPEED_TO_GAME = 1 / GAME_ATTACK_SPEED_TO_REAL;

        public const float REAL_PERCENT_TO_GAME = 1 / GAME_PERCENT_TO_REAL;
    }
}
