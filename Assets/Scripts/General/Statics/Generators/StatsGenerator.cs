using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public static class StatsGenerator
    {
        public static StatPacket ComputeStatPacketFromAttributePacket(BaseAttributePacket baseAttributes)
        {
            var stats = new StatPacket();

            stats.maxHealth = CalculateAttrStatMaxHealth(baseAttributes.Strength());
            stats.maxMana = CalculateAttrStatMaxMana(baseAttributes.Intelligence());

            stats.currHealth = stats.maxHealth;
            stats.currMana = stats.maxMana;

            stats.attackDamage = CalculateAttrStatAttackDamage(baseAttributes.Strength(), baseAttributes.Agility());
            stats.attackSpeed = CalculateStatAttackSpeed(baseAttributes.Agility());
            stats.moveSpeed = CalculateStatMoveSpeed(baseAttributes.Strength(), baseAttributes.Agility());

            stats.armor = CalculateStatArmor(baseAttributes.Strength(), baseAttributes.Agility());
            stats.physicalResistance = CalculateStatPhysicalResistance(baseAttributes.Strength(), baseAttributes.Agility());
            stats.negation = CalculateStatNegation(baseAttributes.Intelligence());
            stats.magicResistance = CalculateStatMagicResistance(baseAttributes.Intelligence());
            stats.debuffResistance = CalculateStatDebuffResistance(baseAttributes.Strength(), baseAttributes.Agility(), baseAttributes.Intelligence());

            stats.healthRegen = CalculateStatHealthRegen(baseAttributes.Strength());
            stats.manaRegen = CalculateStatManaRegen(baseAttributes.Intelligence());

            stats.rotationSpeed = stats.moveSpeed + StatsParameters.BASE_ROTATION_SPEED;

            return stats;
        }

        private static float ValidateStat(float value, float min, float max)
        {
            return Mathf.Max(Mathf.Min(max, value), min);
        }
        
        #region Stat Generation from ATTRIBUTES ONLY

        public static float CalculateAttrStatMaxHealth(int strength)
        {
            return ValidateStat(strength * AttributesParameters.STR_TO_MAX_HEALTH, StatsParameters.MIN_MAX_HEALTH, StatsParameters.MAX_MAX_HEALTH);
        }
        public static float CalculateAttrStatMaxMana(int intelligence)
        {
            return ValidateStat(intelligence * AttributesParameters.INT_TO_MAX_MANAPOOL, StatsParameters.MIN_MAX_MANA, StatsParameters.MAX_MAX_MANA);
        }

        // We do not need generators for currHealth or currMana, because they reflect maxHealth and maxMana
        
        public static float CalculateAttrStatAttackDamage(int strength, int agility)
        {
            return ValidateStat(strength * AttributesParameters.STR_TO_ATTACKDAMAGE + agility * AttributesParameters.AGI_TO_ATTACKDAMAGE, StatsParameters.MIN_ATTACK_DAMAGE,
                StatsParameters.MAX_ATTACK_DAMAGE);
        }
        public static float CalculateStatAttackSpeed(int agility)
        {
            return ValidateStat((agility + StatsParameters.BASE_ATTACK_SPEED) / (StatsParameters.BASE_ATTACK_TIME * 100), StatsParameters.MIN_ATTACK_SPEED, StatsParameters.MAX_ATTACK_SPEED);
        }
        public static float CalculateStatMoveSpeed(int strength, int agility)
        {
            return ValidateStat(strength * AttributesParameters.STR_TO_MOVESPEED + agility * AttributesParameters.AGI_TO_MOVESPEED + StatsParameters.BASE_MOVESPEED, StatsParameters.MIN_MOVESPEED,
                StatsParameters.MAX_MOVESPEED);
        }

        public static float CalculateStatArmor(int strength, int agility)
        {
            return ValidateStat(strength * AttributesParameters.STR_TO_ARMOR + agility * AttributesParameters.AGI_TO_ARMOR, StatsParameters.MIN_ARMOR, StatsParameters.MAX_ARMOR);
        }
        public static float CalculateStatPhysicalResistance(int strength, int agility)
        {
            return ValidateStat(
                strength * AttributesParameters.STR_TO_ARMOR + agility * AttributesParameters.AGI_TO_ARMOR * AttributesParameters.ARMOR_TO_PHYSRESIST +
                StatsParameters.BASE_PHYSICAL_RESIST, StatsParameters.MIN_PHYS_RESIST, StatsParameters.MAX_PHYS_RESIST);
        }
        public static float CalculateStatNegation(int intelligence)
        {
            return ValidateStat(intelligence * AttributesParameters.INT_TO_NEGATION, StatsParameters.MIN_NEGATION, StatsParameters.MAX_NEGATION);
        }
        public static float CalculateStatMagicResistance(int intelligence)
        {
            return ValidateStat(intelligence * AttributesParameters.INT_TO_NEGATION * AttributesParameters.NEGATION_TO_MAGRESIST + StatsParameters.BASE_MAGIC_RESIST,
                StatsParameters.MIN_MAG_RESIST, StatsParameters.MAX_MAG_RESIST);
        }
        public static float CalculateStatDebuffResistance(int strength, int agility, int intelligence)
        {
            return ValidateStat((strength * AttributesParameters.STR_TO_ARMOR + agility * AttributesParameters.AGI_TO_ARMOR)
                                * AttributesParameters.ARMOR_TO_PHYSRESIST * AttributesParameters.PHYSRESIST_TO_DEBUFFRESIST +
                                intelligence * AttributesParameters.INT_TO_NEGATION * AttributesParameters.NEGATION_TO_MAGRESIST * AttributesParameters.MAGRESIST_TO_DEBUGGRESIST,
                StatsParameters.MIN_DEBUFF_RESIST, StatsParameters.MAX_DEBUFF_RESIST);
        }

        public static float CalculateStatHealthRegen(int strength)
        {
            return ValidateStat(strength * AttributesParameters.STR_TO_HEALTHREGEN, StatsParameters.MIN_HEALTH_REGEN, StatsParameters.MAX_HEALTH_REGEN);
        }
        public static float CalculateStatManaRegen(int intelligence)
        {
            return ValidateStat(intelligence * AttributesParameters.INT_TO_MANAREGEN, StatsParameters.MIN_MANA_REGEN, StatsParameters.MAX_MANA_REGEN);
        }
        
        #endregion
        
        #region Stat Packet Manipulation from ADDITIONAL and STATS and/or ATTRIBUTES

        public static void ManipulateStatMaxHealth(StatPacket stats, float additional)
        {
            float previous = stats.maxHealth;
            
            stats.maxHealth = ValidateStat(stats.maxHealth + additional, StatsParameters.MIN_MAX_HEALTH, StatsParameters.MAX_MAX_HEALTH);
            stats.currHealth = stats.currHealth * stats.maxHealth / previous;
        }

        public static void ManipulateStatMaxMana(StatPacket stats, float additional)
        {
            float previous = stats.maxMana;
            
            stats.maxMana = ValidateStat(stats.maxMana + additional, StatsParameters.MIN_MAX_MANA, StatsParameters.MAX_MAX_MANA);
            stats.currMana = stats.currMana * stats.maxMana / previous;
        }
        
        public static void ManipulateStatAttackDamage(StatPacket stats, float additional)
        {
            stats.attackDamage = ValidateStat(stats.attackDamage + additional, StatsParameters.MIN_ATTACK_DAMAGE, StatsParameters.MAX_ATTACK_DAMAGE);
        }
        
        public static void ManipulateStatAttackSpeed(StatPacket stats, BaseAttributePacket baseAttributes, float additional, float multiplier = 0f)
        {
            float value = (StatsParameters.BASE_ATTACK_SPEED + baseAttributes.Agility() + additional * StatsParameters.GAME_ATTACK_SPEED_TO_REAL) * (1 + multiplier) / (100f * StatsParameters.BASE_ATTACK_TIME);
            stats.attackSpeed = ValidateStat(value, StatsParameters.MIN_ATTACK_SPEED, StatsParameters.MAX_ATTACK_SPEED);
        }
        
        public static void ManipulateStatMoveSpeed(StatPacket stats, BaseAttributePacket baseAttributes, float additional, float multiplier = 0f)
        {
            float value = (StatsParameters.BASE_MOVESPEED + baseAttributes.Strength() * AttributesParameters.STR_TO_MOVESPEED +
                           baseAttributes.Agility() * AttributesParameters.AGI_TO_MOVESPEED + additional * StatsParameters.GAME_MOVESPEED_TO_REAL) * (1 + multiplier);
            stats.moveSpeed = ValidateStat(value, StatsParameters.MIN_MOVESPEED, StatsParameters.MAX_MOVESPEED);
            stats.rotationSpeed = ValidateStat(stats.moveSpeed + StatsParameters.BASE_ROTATION_SPEED, StatsParameters.MIN_ROTATION_SPEED, StatsParameters.MAX_ROTATION_SPEED);
        }
        
        public static void ManipulateStatArmor(StatPacket stats, float additional)
        {
            stats.armor = ValidateStat(stats.armor + additional, StatsParameters.MIN_ARMOR, StatsParameters.MAX_ARMOR);
        }
        
        public static void ManipulateStatPhysResist(StatPacket stats, float additional)
        {
            stats.physicalResistance = ValidateStat(stats.physicalResistance + additional * StatsParameters.GAME_PERCENT_TO_REAL, StatsParameters.MIN_PHYS_RESIST, StatsParameters.MAX_PHYS_RESIST);
        }
        
        public static void ManipulateStatNegation(StatPacket stats, float additional)
        {
            stats.negation = ValidateStat(stats.negation + additional, StatsParameters.MIN_NEGATION, StatsParameters.MAX_NEGATION);
        }
        
        public static void ManipulateStatMagResist(StatPacket stats, float additional)
        {
            stats.magicResistance = ValidateStat(stats.magicResistance + additional * StatsParameters.GAME_PERCENT_TO_REAL, StatsParameters.MIN_MAG_RESIST, StatsParameters.MAX_MAG_RESIST);
        }
        
        public static void ManipulateStatHealthRegen(StatPacket stats, float additional)
        {
            stats.healthRegen = ValidateStat(stats.healthRegen + additional, StatsParameters.MIN_HEALTH_REGEN, StatsParameters.MAX_HEALTH_REGEN);
        }
        
        public static void ManipulateStatManaRegen(StatPacket stats, float additional)
        {
            stats.manaRegen = ValidateStat(stats.manaRegen + additional, StatsParameters.MIN_MANA_REGEN, StatsParameters.MAX_MANA_REGEN);
        }
        
        public static void ManipulateStatDebuffResist(StatPacket stats, float additional)
        {
            stats.debuffResistance = ValidateStat(stats.debuffResistance + additional * StatsParameters.GAME_PERCENT_TO_REAL, StatsParameters.MIN_DEBUFF_RESIST, StatsParameters.MAX_DEBUFF_RESIST);
        }
        
        #endregion

        #region Reverse Stat Generation
        // For finding attributes from stats, we only need to consider 3 stats (where each stat originates from a single attribute)

        public static int ComputeUnspecifiedAttributeSumFromStatPacket(StatPacket stats)
        {
            float attributes = 0f;

            attributes += stats.maxHealth / AttributesParameters.STR_TO_MAX_HEALTH;
            attributes += stats.maxMana / AttributesParameters.INT_TO_MAX_MANAPOOL;

            attributes += stats.attackDamage / (AttributesParameters.STR_TO_ATTACKDAMAGE + AttributesParameters.AGI_TO_ATTACKDAMAGE);
            attributes += stats.attackSpeed * 100f * StatsParameters.BASE_ATTACK_TIME - StatsParameters.BASE_ATTACK_SPEED;
            attributes += (stats.moveSpeed - StatsParameters.BASE_MOVESPEED) / (AttributesParameters.STR_TO_MOVESPEED + AttributesParameters.AGI_TO_MOVESPEED);
            attributes += stats.armor / (AttributesParameters.STR_TO_ARMOR + AttributesParameters.AGI_TO_ARMOR);
            attributes += stats.physicalResistance / AttributesParameters.ARMOR_TO_PHYSRESIST / (AttributesParameters.STR_TO_ARMOR + AttributesParameters.AGI_TO_ARMOR);
            attributes += stats.negation / AttributesParameters.INT_TO_NEGATION;
            attributes += stats.magicResistance / AttributesParameters.NEGATION_TO_MAGRESIST / AttributesParameters.INT_TO_NEGATION;
            attributes += stats.healthRegen / AttributesParameters.STR_TO_HEALTHREGEN;
            attributes += stats.manaRegen / AttributesParameters.INT_TO_MANAREGEN;
            attributes += stats.debuffResistance / (AttributesParameters.ARMOR_TO_PHYSRESIST + AttributesParameters.NEGATION_TO_MAGRESIST) / 
                          (Mathf.Max(AttributesParameters.ARMOR_TO_PHYSRESIST, AttributesParameters.NEGATION_TO_MAGRESIST) - 
                           Mathf.Min(AttributesParameters.ARMOR_TO_PHYSRESIST, AttributesParameters.NEGATION_TO_MAGRESIST));

            return Mathf.CeilToInt(attributes);
        }

        public static int ComputeUnspecifiedAttributeSumFromModifierValues(Dictionary<CharacterModifierTag, float> modifierAmounts)
        {
            float impreciseAttributes = 0;

            foreach (CharacterModifierTag modifier in modifierAmounts.Keys)
            {
                switch (modifier)
                {

                    case CharacterModifierTag.ModifyCurrentHealth:
                        impreciseAttributes += modifierAmounts[modifier] / (AttributesParameters.STR_TO_ATTACKDAMAGE + AttributesParameters.AGI_TO_ATTACKDAMAGE);
                        break;
                    case CharacterModifierTag.ModifyCurrentMana:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.INT_TO_MAX_MANAPOOL;
                        break;
                    case CharacterModifierTag.ModifyMovespeed:
                        impreciseAttributes += modifierAmounts[modifier] / (AttributesParameters.STR_TO_MOVESPEED + AttributesParameters.AGI_TO_MOVESPEED);
                        break;
                    case CharacterModifierTag.ModifyAttackSpeed:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.AGI_TO_ATTACKSPEED;
                        break;
                    case CharacterModifierTag.ModifyArmor:
                        impreciseAttributes += modifierAmounts[modifier] / (AttributesParameters.STR_TO_ARMOR + AttributesParameters.AGI_TO_ARMOR);
                        break;
                    case CharacterModifierTag.ModifyNegation:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.INT_TO_NEGATION;
                        break;
                    case CharacterModifierTag.ModifyMagicalResistance:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.NEGATION_TO_MAGRESIST / AttributesParameters.INT_TO_NEGATION;
                        break;
                    case CharacterModifierTag.ModifyPhysicalResistance:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.ARMOR_TO_PHYSRESIST / (AttributesParameters.STR_TO_ARMOR + AttributesParameters.AGI_TO_ARMOR);
                        break;
                    case CharacterModifierTag.ModifyDebuffResistance:
                        impreciseAttributes += modifierAmounts[modifier] / (AttributesParameters.ARMOR_TO_PHYSRESIST + AttributesParameters.NEGATION_TO_MAGRESIST) / 
                                               (Mathf.Max(AttributesParameters.ARMOR_TO_PHYSRESIST, AttributesParameters.NEGATION_TO_MAGRESIST) - 
                                                Mathf.Min(AttributesParameters.ARMOR_TO_PHYSRESIST, AttributesParameters.NEGATION_TO_MAGRESIST));
                        break;
                    case CharacterModifierTag.ModifyAttackDamage:
                        impreciseAttributes += modifierAmounts[modifier] / (AttributesParameters.STR_TO_ATTACKDAMAGE + AttributesParameters.AGI_TO_ATTACKDAMAGE);
                        break;
                    case CharacterModifierTag.ModifyMaxHealth:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.STR_TO_MAX_HEALTH;
                        break;
                    case CharacterModifierTag.ModifyMaxMana:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.INT_TO_MAX_MANAPOOL;
                        break;
                    case CharacterModifierTag.ModifyHealthRegen:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.STR_TO_HEALTHREGEN;
                        break;
                    case CharacterModifierTag.ModifyManaRegen:
                        impreciseAttributes += modifierAmounts[modifier] / AttributesParameters.INT_TO_MANAREGEN;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return Mathf.CeilToInt(impreciseAttributes);
        }

        public static int ComputeUnspecifiedAttributeSumFromAbilities(BaseAbilityData[] abilities)
        {
            return 0;
        }
    
        
        #endregion
    }
}
