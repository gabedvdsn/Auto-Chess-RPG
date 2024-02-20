using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace AutoChessRPG
{
    public static class AttributesManager
    {
        // Examples given are for character with 20 of each stat
        
        private const float STR_TO_MAX_HEALTH = 25f;  // 500 health
        
        private const float INT_TO_MAX_MANAPOOL = 22f;  // 440 mana

        private const float STR_TO_ATTACKDAMAGE = .75f;  // 15
        private const float AGI_TO_ATTACKDAMAGE = 2.25f;  // 45 -> 60 total damage per attack
        
        private const float AGI_TO_ATTACKSPEED = .06f;  // 1.2 attacks per second
        
        private const float STR_TO_MOVESPEED = -.03f;  // -.6
        private const float AGI_TO_MOVESPEED = .09f;  // 1.8 -> 1.2 total units per second
        
        private const float STR_TO_ARMOR = .15f;  // 3
        private const float AGI_TO_ARMOR = .4f;  // 8 -> 11 total armor

        private const float ARMOR_TO_PHYSRESIST = .8f / 100f;  // 8.8% or .088 phys resist
            
        private const float INT_TO_NEGATION = .35f;  // 7 negation (magic armor)

        private const float NEGATION_TO_MAGRESIST = 1.25f / 100f;  // 8.75% or .0875 mag resist
        
        private const float STR_TO_HEALTHREGEN = .4f;  // 8 health regen
        
        private const float INT_TO_MANAREGEN = .55f;  // 11 mana regen

        private const float PHYSRESIST_TO_DEBUFFRESIST = .5f;  // .044
        private const float MAGRESIST_TO_DEBUGGRESIST = .5f;  // .04375 -> 8.775% or .08775 total debugg resist

        public static StatPacket ComputeStatPacketFromAttributePacket(AttributePacket attributes)
        {
            StatPacket stats = new StatPacket();
            
            stats.maxHealth = attributes.Strength() * STR_TO_MAX_HEALTH;
            stats.maxManaPool = attributes.Intelligence() * INT_TO_MAX_MANAPOOL;

            stats.currHealth = stats.maxHealth;
            stats.currMana = stats.maxManaPool;

            stats.attackDamage = attributes.Strength() * STR_TO_ATTACKDAMAGE + attributes.Agility() * AGI_TO_ATTACKDAMAGE;
            stats.attackSpeed = attributes.Agility() * AGI_TO_ATTACKSPEED;
            stats.moveSpeed = attributes.Strength() * STR_TO_MOVESPEED + attributes.Agility() * AGI_TO_MOVESPEED;
            
            stats.armor = attributes.Strength() * STR_TO_ARMOR + attributes.Agility() * AGI_TO_ARMOR;
            stats.physicalResistance = stats.armor * ARMOR_TO_PHYSRESIST;
            stats.negation = attributes.Intelligence() * INT_TO_NEGATION;
            stats.magicResistance = stats.negation * NEGATION_TO_MAGRESIST;
            stats.debuffResistance = stats.physicalResistance * PHYSRESIST_TO_DEBUFFRESIST +
                                     stats.magicResistance * MAGRESIST_TO_DEBUGGRESIST;
            
            stats.healthRegen = attributes.Strength() * STR_TO_HEALTHREGEN;
            stats.manaRegen = attributes.Intelligence() * INT_TO_MANAREGEN;

            return stats;
        }
        
        private static class MinionAttributesManager
        {
            
        }

        private static class HeroAttributesManager
        {
            #region Artificer Stat Coefficients
            private const float ARTIFICER_HEALTH_COEFFICIENT = .9f;
            private const float ARTIFICER_MANA_POOL_COEFFICIENT = .9f;
            private const float ARTIFICER_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float ARTIFICER_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float ARTIFICER_MOVE_SPEED_COEFFICIENT = .9f;
            private const float ARTIFICER_ARMOR_COEFFICIENT = .9f;
            private const float ARTIFICER_PHYS_RESIST_COEFFICIENT = .9f;
            private const float ARTIFICER_MAG_RESIST_COEFFICIENT = .9f;
            private const float ARTIFICER_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float ARTIFICER_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Alchemist Stat Coefficients
            private const float ALCHEMIST_HEALTH_COEFFICIENT = .9f;
            private const float ALCHEMIST_MANA_POOL_COEFFICIENT = .9f;
            private const float ALCHEMIST_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float ALCHEMIST_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float ALCHEMIST_MOVE_SPEED_COEFFICIENT = .9f;
            private const float ALCHEMIST_ARMOR_COEFFICIENT = .9f;
            private const float ALCHEMIST_PHYS_RESIST_COEFFICIENT = .9f;
            private const float ALCHEMIST_MAG_RESIST_COEFFICIENT = .9f;
            private const float ALCHEMIST_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float ALCHEMIST_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Bard Stat Coefficients
            private const float BARD_HEALTH_COEFFICIENT = .9f;
            private const float BARD_MANA_POOL_COEFFICIENT = .9f;
            private const float BARD_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float BARD_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float BARD_MOVE_SPEED_COEFFICIENT = .9f;
            private const float BARD_ARMOR_COEFFICIENT = .9f;
            private const float BARD_PHYS_RESIST_COEFFICIENT = .9f;
            private const float BARD_MAG_RESIST_COEFFICIENT = .9f;
            private const float BARD_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float BARD_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Cleric Stat Coefficients
            private const float CLERIC_HEALTH_COEFFICIENT = .9f;
            private const float CLERIC_MANA_POOL_COEFFICIENT = .9f;
            private const float CLERIC_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float CLERIC_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float CLERIC_MOVE_SPEED_COEFFICIENT = .9f;
            private const float CLERIC_ARMOR_COEFFICIENT = .9f;
            private const float CLERIC_PHYS_RESIST_COEFFICIENT = .9f;
            private const float CLERIC_MAG_RESIST_COEFFICIENT = .9f;
            private const float CLERIC_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float CLERIC_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Druid Stat Coefficients
            private const float DRUID_HEALTH_COEFFICIENT = .9f;
            private const float DRUID_MANA_POOL_COEFFICIENT = .9f;
            private const float DRUID_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float DRUID_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float DRUID_MOVE_SPEED_COEFFICIENT = .9f;
            private const float DRUID_ARMOR_COEFFICIENT = .9f;
            private const float DRUID_PHYS_RESIST_COEFFICIENT = .9f;
            private const float DRUID_MAG_RESIST_COEFFICIENT = .9f;
            private const float DRUID_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float DRUID_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Hunter Stat Coefficients
            private const float HUNTER_HEALTH_COEFFICIENT = .9f;
            private const float HUNTER_MANA_POOL_COEFFICIENT = .9f;
            private const float HUNTER_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float HUNTER_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float HUNTER_MOVE_SPEED_COEFFICIENT = .9f;
            private const float HUNTER_ARMOR_COEFFICIENT = .9f;
            private const float HUNTER_PHYS_RESIST_COEFFICIENT = .9f;
            private const float HUNTER_MAG_RESIST_COEFFICIENT = .9f;
            private const float HUNTER_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float HUNTER_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Warlock Stat Coefficients
            private const float WARLOCK_HEALTH_COEFFICIENT = .9f;
            private const float WARLOCK_MANA_POOL_COEFFICIENT = .9f;
            private const float WARLOCK_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float WARLOCK_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float WARLOCK_MOVE_SPEED_COEFFICIENT = .9f;
            private const float WARLOCK_ARMOR_COEFFICIENT = .9f;
            private const float WARLOCK_PHYS_RESIST_COEFFICIENT = .9f;
            private const float WARLOCK_MAG_RESIST_COEFFICIENT = .9f;
            private const float WARLOCK_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float WARLOCK_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            #region Warrior Stat Coefficients
            private const float WARRIOR_HEALTH_COEFFICIENT = .9f;
            private const float WARRIOR_MANA_POOL_COEFFICIENT = .9f;
            private const float WARRIOR_ATTACK_DAMAGE_COEFFICIENT = .9f;
            private const float WARRIOR_ATTACK_SPEED_COEFFICIENT = .9f;
            private const float WARRIOR_MOVE_SPEED_COEFFICIENT = .9f;
            private const float WARRIOR_ARMOR_COEFFICIENT = .9f;
            private const float WARRIOR_PHYS_RESIST_COEFFICIENT = .9f;
            private const float WARRIOR_MAG_RESIST_COEFFICIENT = .9f;
            private const float WARRIOR_HEALTH_REGEN_COEFFICIENT = .9f;
            private const float WARRIOR_MANA_REGEN_COEFFICIENT = .9f;
            #endregion
            
            private static Dictionary<Class, Dictionary<Stat, float>> StatByClassCoefficients = new Dictionary<Class, Dictionary<Stat, float>>()
            {
                {Class.Artificer, new Dictionary<Stat, float>()
                {
                    {Stat.Health, ARTIFICER_HEALTH_COEFFICIENT },
                    {Stat.Health, ARTIFICER_MANA_POOL_COEFFICIENT },
                    {Stat.Health, ARTIFICER_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, ARTIFICER_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, ARTIFICER_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, ARTIFICER_ARMOR_COEFFICIENT },
                    {Stat.Health, ARTIFICER_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, ARTIFICER_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, ARTIFICER_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, ARTIFICER_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Alchemist, new Dictionary<Stat, float>()
                {
                    {Stat.Health, ALCHEMIST_HEALTH_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_MANA_POOL_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_ARMOR_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, ALCHEMIST_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Bard, new Dictionary<Stat, float>()
                {
                    {Stat.Health, BARD_HEALTH_COEFFICIENT },
                    {Stat.Health, BARD_MANA_POOL_COEFFICIENT },
                    {Stat.Health, BARD_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, BARD_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, BARD_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, BARD_ARMOR_COEFFICIENT },
                    {Stat.Health, BARD_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, BARD_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, BARD_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, BARD_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Cleric, new Dictionary<Stat, float>()
                {
                    {Stat.Health, CLERIC_HEALTH_COEFFICIENT },
                    {Stat.Health, CLERIC_MANA_POOL_COEFFICIENT },
                    {Stat.Health, CLERIC_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, CLERIC_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, CLERIC_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, CLERIC_ARMOR_COEFFICIENT },
                    {Stat.Health, CLERIC_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, CLERIC_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, CLERIC_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, CLERIC_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Druid, new Dictionary<Stat, float>()
                {
                    {Stat.Health, DRUID_HEALTH_COEFFICIENT },
                    {Stat.Health, DRUID_MANA_POOL_COEFFICIENT },
                    {Stat.Health, DRUID_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, DRUID_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, DRUID_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, DRUID_ARMOR_COEFFICIENT },
                    {Stat.Health, DRUID_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, DRUID_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, DRUID_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, DRUID_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Hunter, new Dictionary<Stat, float>()
                {
                    {Stat.Health, HUNTER_HEALTH_COEFFICIENT },
                    {Stat.Health, HUNTER_MANA_POOL_COEFFICIENT },
                    {Stat.Health, HUNTER_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, HUNTER_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, HUNTER_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, HUNTER_ARMOR_COEFFICIENT },
                    {Stat.Health, HUNTER_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, HUNTER_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, HUNTER_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, HUNTER_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Warlock, new Dictionary<Stat, float>()
                {
                    {Stat.Health, WARLOCK_HEALTH_COEFFICIENT },
                    {Stat.Health, WARLOCK_MANA_POOL_COEFFICIENT },
                    {Stat.Health, WARLOCK_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, WARLOCK_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, WARLOCK_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, WARLOCK_ARMOR_COEFFICIENT },
                    {Stat.Health, WARLOCK_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, WARLOCK_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, WARLOCK_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, WARLOCK_MANA_REGEN_COEFFICIENT },
                } },
                {Class.Warrior, new Dictionary<Stat, float>()
                {
                    {Stat.Health, WARRIOR_HEALTH_COEFFICIENT },
                    {Stat.Health, WARRIOR_MANA_POOL_COEFFICIENT },
                    {Stat.Health, WARRIOR_ATTACK_DAMAGE_COEFFICIENT },
                    {Stat.Health, WARRIOR_ATTACK_SPEED_COEFFICIENT },
                    {Stat.Health, WARRIOR_MOVE_SPEED_COEFFICIENT },
                    {Stat.Health, WARRIOR_ARMOR_COEFFICIENT },
                    {Stat.Health, WARRIOR_PHYS_RESIST_COEFFICIENT },
                    {Stat.Health, WARRIOR_MAG_RESIST_COEFFICIENT },
                    {Stat.Health, WARRIOR_HEALTH_REGEN_COEFFICIENT },
                    {Stat.Health, WARRIOR_MANA_REGEN_COEFFICIENT },
                } }
            };

            public static float GetStatByClassCoefficient(Class _class, Stat _stat) =>
                StatByClassCoefficients[_class][_stat];

            public static float GetAppliedStatByClassCoefficient(Class _class, Stat _stat, float _statValue) =>
                GetStatByClassCoefficient(_class, _stat) * _statValue;
        }
        
        private static Dictionary<Race, float> RaceStatCoefficients = new Dictionary<Race, float>()
        {

        };
        
        public static Dictionary<Stat, float> AttributesToStats(Dictionary<Attribute, int> attributes)
        {
            return new Dictionary<Stat, float>()
            {
                { Stat.Health, ComputeHealthStat(attributes[Attribute.Strength])}
            };
        }

        private static Dictionary<Stat, float> GetDefaultStatsFromAttributes(Dictionary<Attribute, int> attributes)
        {
            Dictionary<Stat, float> stats = new Dictionary<Stat, float>();

            if (attributes.TryGetValue(Attribute.Strength, out var strengthAttribute))
            {
                stats[Stat.Health] = ComputeHealthStat(strengthAttribute);
            }

            if (attributes.TryGetValue(Attribute.Agility, out var agilityAttribute))
            {
                stats[Stat.MoveSpeed] = ComputeMoveSpeedStat(agilityAttribute);
                stats[Stat.AttackSpeed] = ComputeAttackSpeedStat(agilityAttribute);
            }

            if (attributes.TryGetValue(Attribute.Intelligence, out var intAttribute))
            {
                stats[Stat.ManaPool] = ComputeManapoolStat(intAttribute);
            }

            return stats;
        }

        public static float ComputeHealthStat(int strength) => strength;  // * STRENGTH_TO_HEALTH;

        public static float ComputeManapoolStat(int intelligence) => intelligence;  // * INT_TO_MANAPOOL;

        public static float ComputeAttackDamageStat(int strength, int agility, int intelligence)
        {
            return 0f;
        }

        public static float ComputeMoveSpeedStat(int agility) => agility;  // * AGI_TO_MOVESPEED;

        public static float ComputeAttackSpeedStat(int agility) => agility;  // * AGI_TO_ATTACKSPEED;
    }
}
