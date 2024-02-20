using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class StatPacket
    {
        public float maxHealth;
        public float maxManaPool;
        
        public float currHealth;
        public float currMana;
        public float attackDamage;
        public float attackSpeed;
        public float moveSpeed;
        public float armor;
        public float physicalResistance;
        public float negation;
        public float magicResistance;
        public float healthRegen;
        public float manaRegen;
        public float debuffResistance;

        public StatPacket() { }

        public bool ApplyModifier(CharacterModifierTag modifier, float value)
        {
            switch (modifier)
            {
                case CharacterModifierTag.DecreaseArmor:
                    armor -= value;
                    break;
                case CharacterModifierTag.DecreaseAttackDamage:
                    attackDamage -= value;
                    break;
                case CharacterModifierTag.DecreaseCurrentHealth:
                    currHealth -= value;
                    break;
                case CharacterModifierTag.DecreaseCurrentMana:
                    currMana -= value;
                    break;
                case CharacterModifierTag.DecreaseDebuffResistance:
                    debuffResistance -= value;
                    break;
                case CharacterModifierTag.DecreaseHealthRegen:
                    healthRegen -= value;
                    break;
                case CharacterModifierTag.DecreaseMagicalResistance:
                    magicResistance -= value;
                    break;
                case CharacterModifierTag.DecreaseManaRegen:
                    manaRegen -= value;
                    break;
                case CharacterModifierTag.DecreaseMaxHealth:
                    maxHealth -= value;
                    currHealth *= maxHealth / (maxHealth + value);
                    break;
                case CharacterModifierTag.DecreaseMaxManapool:
                    maxManaPool -= value;
                    currMana *= maxManaPool / (maxManaPool + value);
                    break;
                case CharacterModifierTag.DecreaseMovementSpeed:
                    moveSpeed -= value;
                    break;
                case CharacterModifierTag.DecreaseNegation:
                    negation -= value;
                    break;
                case CharacterModifierTag.DecreasePhysicalResistance:
                    physicalResistance -= value;
                    break;
                case CharacterModifierTag.ReduceAttackSpeed:
                    attackSpeed -= value;
                    break;
                default:
                    return false;
            }

            return true;
        }

        public void ApplyAttributePacket(AttributePacket attributes)
        {
            
        }
        
    }
    
}