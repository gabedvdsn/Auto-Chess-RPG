namespace AutoChessRPG
{
    public static class PowerGenerator
    {
        
        #region Character Power
        
        private const float CHARACTER_POWER_COEFFICIENT_RARITY = .15f;  // for each level of rarity, from levels 1 (0) -> 5 (4) => 1 + RARITY * COEFFICIENT
        private const float CHARACTER_POWER_COEFFICIENT_LEVEL = .25f;  // for each level, from levels 1 (0) -> 5 (4) => 1 + LEVEL * COEFFICIENT

        private static float CalculateCharacterRarityPowerMultiplier(int rarity) => 1 + rarity * CHARACTER_POWER_COEFFICIENT_RARITY;

        private static float CalculateCharacterLevelPowerMultiplier(int level) => 1 + level * CHARACTER_POWER_COEFFICIENT_LEVEL;

        public static float GetCharacterPower(Character character)
        {
            /*
             * For Character, we sum attributes (as opposed to considering stats) because stats are consequences of attributes
             */
            
            float sumPower = 0f;

            // Sum of attributes
            sumPower += character.GetCharacterData().GetAttributes().Strength();
            sumPower += character.GetCharacterData().GetAttributes().Agility();
            sumPower += character.GetCharacterData().GetAttributes().Intelligence();

            // Apply PowerPacket multipliers
            return sumPower * CalculateCharacterRarityPowerMultiplier(character.GetCharacterPowerPacket().rarity) * CalculateCharacterLevelPowerMultiplier(character.GetCharacterPowerPacket().level);
        }
        
        #endregion
        
        #region Item Power

        private const float ITEM_POWER_COEFFICIENT_RARITY = .25f;
        private const float ITEM_POWER_COEFFICIENT_LEVEL = .35f;
        
        private static float CalculateItemRarityPowerMultiplier(int rarity) => 1 + rarity * ITEM_POWER_COEFFICIENT_RARITY;

        private static float CalculateItemLevelPowerMultiplier(int level) => 1 + level * ITEM_POWER_COEFFICIENT_LEVEL;
        
        public static int GetItemPower(ItemData item)
        {
            /*
             * For items, we sum attributes AND consider stats, because (in this case) stats are not consequences of attributes
             */
            int sumPower = 0;

            // power of attached attributes
            sumPower += item.GetAttachedAttributes().Strength();
            sumPower += item.GetAttachedAttributes().Agility();
            sumPower += item.GetAttachedAttributes().Intelligence();

            // estimated power of attached stats
            sumPower += StatsGenerator.ComputeUnspecifiedAttributeSumFromStatPacket(item.GetAttachedStats());
            
            // estimated power of attached abilities
            sumPower += 

            // Apply PowerPacket multipliers
            return sumPower * CalculateCharacterRarityPowerMultiplier(character.GetCharacterPowerPacket().rarity) * CalculateCharacterLevelPowerMultiplier(character.GetCharacterPowerPacket().level);


        #endregion
    }
}
