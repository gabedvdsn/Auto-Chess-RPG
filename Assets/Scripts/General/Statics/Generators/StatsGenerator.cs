using UnityEngine;

namespace AutoChessRPG
{
    public static class StatsGenerator
    {
        public static StatPacket ComputeStatPacketFromAttributePacket(AttributePacket attributes)
        {
            var stats = new StatPacket();

            stats.maxHealth = attributes.Strength() * AttributesParameters.STR_TO_MAX_HEALTH;
            stats.maxManaPool = attributes.Intelligence() * AttributesParameters.INT_TO_MAX_MANAPOOL;

            stats.currHealth = stats.maxHealth;
            stats.currMana = stats.maxManaPool;

            stats.attackDamage = attributes.Strength() * AttributesParameters.STR_TO_ATTACKDAMAGE + attributes.Agility() * AttributesParameters.AGI_TO_ATTACKDAMAGE;
            stats.attackSpeed = attributes.Agility() * AttributesParameters.AGI_TO_ATTACKSPEED;
            stats.moveSpeed = attributes.Strength() * AttributesParameters.STR_TO_MOVESPEED + attributes.Agility() * AttributesParameters.AGI_TO_MOVESPEED;

            stats.armor = attributes.Strength() * AttributesParameters.STR_TO_ARMOR + attributes.Agility() * AttributesParameters.AGI_TO_ARMOR;
            stats.physicalResistance = stats.armor * AttributesParameters.ARMOR_TO_PHYSRESIST;
            stats.negation = attributes.Intelligence() * AttributesParameters.INT_TO_NEGATION;
            stats.magicResistance = stats.negation * AttributesParameters.NEGATION_TO_MAGRESIST;
            stats.debuffResistance = stats.physicalResistance * AttributesParameters.PHYSRESIST_TO_DEBUFFRESIST +
                                     stats.magicResistance * AttributesParameters.MAGRESIST_TO_DEBUGGRESIST;

            stats.healthRegen = attributes.Strength() * AttributesParameters.STR_TO_HEALTHREGEN;
            stats.manaRegen = attributes.Intelligence() * AttributesParameters.INT_TO_MANAREGEN;

            return stats;
        }

        public static float CalculateStatMaxHealth(int strength)
        {
            return strength * AttributesParameters.STR_TO_MAX_HEALTH;
        }
        public static float CalculateStatMaxMana(int intelligence)
        {
            return intelligence * AttributesParameters.INT_TO_MAX_MANAPOOL;
        }

        // We do not need generators for currHealth or currMana, because they reflect maxHealth and maxMana

        #region Stat Generation
        
        public static float CalculateStatAttackDamage(int strength, int agility)
        {
            return strength * AttributesParameters.STR_TO_ATTACKDAMAGE + agility * AttributesParameters.AGI_TO_ATTACKDAMAGE;
        }
        public static float CalculateStatAttackSpeed(int agility)
        {
            return agility * AttributesParameters.AGI_TO_ATTACKSPEED;
        }
        public static float CalculateStatMoveSpeed(int strength, int agility)
        {
            return strength * AttributesParameters.STR_TO_MOVESPEED + agility * AttributesParameters.AGI_TO_MOVESPEED;
        }

        public static float CalculateStatArmor(int strength, int agility)
        {
            return strength * AttributesParameters.STR_TO_ARMOR + agility * AttributesParameters.AGI_TO_ARMOR;
        }
        public static float CalculateStatPhysicalResistance(int strength, int agility)
        {
            return strength * AttributesParameters.STR_TO_ARMOR + agility * AttributesParameters.AGI_TO_ARMOR * AttributesParameters.ARMOR_TO_PHYSRESIST;
        }
        public static float CalculateStatNegation(int intelligence)
        {
            return intelligence * AttributesParameters.INT_TO_NEGATION;
        }
        public static float CalculateStatMagicResistance(int intelligence)
        {
            return intelligence * AttributesParameters.INT_TO_NEGATION * AttributesParameters.NEGATION_TO_MAGRESIST;
        }
        public static float CalculateStatDebuffResistance(int strength, int agility, int intelligence)
        {
            return strength * AttributesParameters.STR_TO_ARMOR +
                   agility * AttributesParameters.AGI_TO_ARMOR * AttributesParameters.ARMOR_TO_PHYSRESIST * AttributesParameters.PHYSRESIST_TO_DEBUFFRESIST +
                   intelligence * AttributesParameters.INT_TO_NEGATION * AttributesParameters.NEGATION_TO_MAGRESIST * AttributesParameters.MAGRESIST_TO_DEBUGGRESIST;
        }

        public static float CalculateStatHealthRegen(int strength)
        {
            return strength * AttributesParameters.STR_TO_HEALTHREGEN;
        }
        public static float CalculateStatManaRegen(int intelligence)
        {
            return intelligence * AttributesParameters.INT_TO_MANAREGEN;
        }
        
        #endregion

        #region Reverse Stat Generation
        // For finding attributes from stats, we only need to consider 3 stats (where each stat originates from a single attribute)

        public static int ComputeUnspecifiedAttributeSumFromStatPacket(StatPacket stats)
        {
            float attributes = 0f;

            attributes += stats.maxHealth / AttributesParameters.STR_TO_MAX_HEALTH;
            attributes += stats.maxManaPool / AttributesParameters.INT_TO_MAX_MANAPOOL;

            attributes += stats.attackDamage / (AttributesParameters.STR_TO_ATTACKDAMAGE + AttributesParameters.AGI_TO_ATTACKDAMAGE);
            attributes += stats.attackSpeed / AttributesParameters.AGI_TO_ATTACKSPEED;
            attributes += stats.moveSpeed / (AttributesParameters.STR_TO_MOVESPEED + AttributesParameters.AGI_TO_MOVESPEED);
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

        public static int ComputeUnspecifiedAttributeSumFromAbilities(AbilityData[] abilities)
        {
            
        }
    
        
        #endregion
    }
}
