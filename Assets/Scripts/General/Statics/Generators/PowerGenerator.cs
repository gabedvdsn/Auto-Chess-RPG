using System.Collections.Generic;
using UnityEngine;

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
            return sumPower * CalculateCharacterRarityPowerMultiplier((int)character.GetCharacterPowerPacket().basePacket.rarity) * CalculateCharacterLevelPowerMultiplier(character.GetCharacterPowerPacket().level);
        }
        
        #endregion
        
        #region Item Power

        private const float ITEM_POWER_COEFFICIENT_RARITY = .25f;
        private const float ITEM_POWER_COEFFICIENT_LEVEL = .35f;
        
        private static float CalculateItemRarityPowerMultiplier(int rarity) => 1 + rarity * ITEM_POWER_COEFFICIENT_RARITY;

        private static float CalculateItemLevelPowerMultiplier(int level) => 1 + level * ITEM_POWER_COEFFICIENT_LEVEL;

        public static int GetItemPower(RealItemData realItem)
        {
            /*
             * For items, we sum attributes AND consider stats, because (in this case) stats are not consequences of attributes
             * For attachedAbilities, we do consider their power but NOT their power level multipliers
             */
            int sumPower = 0;

            // power of attached attributes
            sumPower += realItem.GetAttachedAttributes().GetAttributeSum();

            // estimated power of attached stats
            sumPower += StatsGenerator.ComputeUnspecifiedAttributeSumFromStatPacket(realItem.GetBaseData().GetAttachedStats());

            // estimated power of attached abilities
            foreach (RealAbilityData ability in realItem.GetAttachedAbilities())
            {
                int abilityPower = GetAbilityPower(ability);

                // Remove multipliers
                abilityPower = Mathf.CeilToInt(abilityPower / CalculateAbilityRarityPowerMultiplier((int)ability.GetBasePowerPacket().rarity));
                abilityPower = Mathf.CeilToInt(abilityPower / CalculateAbilityLevelPowerMultiplier(ability.GetPowerPacket().level));

                sumPower += abilityPower;
            }

            // Apply PowerPacket multipliers
            return Mathf.CeilToInt(sumPower * CalculateCharacterRarityPowerMultiplier((int)realItem.GetBasePowerPacket().rarity) *
                                   CalculateCharacterLevelPowerMultiplier(realItem.GetPowerPacket().level));
        }


        #endregion
        
        #region Ability Power
        
        private const float ABILITY_POWER_COEFFICIENT_RARITY = .25f;
        private const float ABILITY_POWER_COEFFICIENT_LEVEL = .35f;
        
        private static float CalculateAbilityRarityPowerMultiplier(int rarity) => 1 + rarity * ABILITY_POWER_COEFFICIENT_RARITY;

        private static float CalculateAbilityLevelPowerMultiplier(int level) => 1 + level * ABILITY_POWER_COEFFICIENT_LEVEL;

        public static int GetAbilityPower(RealAbilityData realAbility)
        {
            /*
             * For items, we sum attributes AND consider stats with regard to attachedEffects ONLY, because (in this case) stats are not consequences of attributes
             */
            
            int sumPower = CalculateEffectsPower(realAbility.GetAllEffects());
            
            // Apply PowerPacket multipliers
            return Mathf.CeilToInt(sumPower * CalculateAbilityRarityPowerMultiplier((int)realAbility.GetBasePowerPacket().rarity) *
                                   CalculateAbilityLevelPowerMultiplier(realAbility.GetPowerPacket().level));
        }

        private static int CalculateEffectsPower(RealEffectData[] effects)
        {
            Dictionary<CharacterModifierTag, float> modifierValues = new Dictionary<CharacterModifierTag, float>();

            foreach (RealEffectData effect in effects)
            {
                if (!modifierValues.ContainsKey(effect.GetBaseData().GetModifier())) modifierValues[effect.GetBaseData().GetModifier()] = 0f;
                
                if (effect.GetBaseData().GetApplyOnce())
                {
                    modifierValues[effect.GetBaseData().GetModifier()] = effect.GetAmount();
                }
                else
                {
                    modifierValues[effect.GetBaseData().GetModifier()] = effect.GetAmount() * effect.GetDuration() / effect.GetTickRate();
                }
            }
            
            return StatsGenerator.ComputeUnspecifiedAttributeSumFromModifierValues(modifierValues);
            
        }
        
        #endregion
        
        #region Other

        private const float POWER_COEFFICIENT_DELTA_CAST_TIME = .15f;
        
        private const float POWER_COEFFICIENT_DELTA_COOLDOWN = .75f;
        
        public static int GetNewPowerFromDeltaCastTime(float currPower, float oldCastTime, float newCastTime)
        {
            float ratio = 1 - newCastTime / oldCastTime;  // >0 is an improvement, <0 is a worsening
            float deltaPowerRatio = Mathf.Max(0, 1 + ratio * POWER_COEFFICIENT_DELTA_CAST_TIME);

            return deltaPowerRatio == 0 ? 1 : Mathf.CeilToInt(currPower * deltaPowerRatio);

        }

        public static int GetNewPowerFromDeltaCooldown(float currPower, float oldCooldown, float newCooldown)
        {
            float ratio = 1 - newCooldown / oldCooldown;  // >0 is an improvement, <0 is a worsening
            float deltaPowerRatio = Mathf.Max(0, 1 + ratio * POWER_COEFFICIENT_DELTA_COOLDOWN);

            return deltaPowerRatio == 0 ? 1 : Mathf.CeilToInt(currPower * deltaPowerRatio);
        }
        
        #endregion
    }
}
