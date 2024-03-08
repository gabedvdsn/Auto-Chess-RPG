using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [Serializable]
    public class StatPacket
    {
        public float maxHealth;
        public float maxMana;
        
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

        public float rotationSpeed;

        public StatPacket() { }

        public bool ApplyModifier(CharacterModifierTag modifier, float value)
        {
            switch (modifier)
            {
                case CharacterModifierTag.ModifyArmor:
                    armor -= value;
                    break;
                case CharacterModifierTag.ModifyAttackDamage:
                    attackDamage -= value;
                    break;
                case CharacterModifierTag.ModifyCurrentHealth:
                    currHealth -= value;
                    break;
                case CharacterModifierTag.ModifyCurrentMana:
                    currMana -= value;
                    break;
                case CharacterModifierTag.ModifyDebuffResistance:
                    debuffResistance -= value;
                    break;
                case CharacterModifierTag.ModifyHealthRegen:
                    healthRegen -= value;
                    break;
                case CharacterModifierTag.ModifyMagicalResistance:
                    magicResistance -= value;
                    break;
                case CharacterModifierTag.ModifyManaRegen:
                    manaRegen -= value;
                    break;
                case CharacterModifierTag.ModifyMaxHealth:
                    maxHealth -= value;
                    currHealth *= maxHealth / (maxHealth + value);
                    break;
                case CharacterModifierTag.ModifyMaxMana:
                    maxMana -= value;
                    currMana *= maxMana / (maxMana + value);
                    break;
                case CharacterModifierTag.ModifyMovespeed:
                    moveSpeed -= value;
                    break;
                case CharacterModifierTag.ModifyNegation:
                    negation -= value;
                    break;
                case CharacterModifierTag.ModifyPhysicalResistance:
                    physicalResistance -= value;
                    break;
                case CharacterModifierTag.ModifyAttackSpeed:
                    attackSpeed -= value;
                    break;
                default:
                    return false;
            }

            return true;
        }

        public void MergeOtherStatPacket(StatPacket other)
        {
            maxHealth += other.maxHealth;
            maxMana += other.maxMana;
            
            attackDamage += other.attackDamage;
            attackSpeed += other.attackSpeed;
            moveSpeed += other.moveSpeed;
            armor += other.armor;
            physicalResistance += other.physicalResistance;
            negation += other.negation;
            magicResistance += other.magicResistance;
            healthRegen += other.healthRegen;
            manaRegen += other.manaRegen;
            debuffResistance += other.debuffResistance;

            rotationSpeed = moveSpeed;
        }

        public override string ToString()
        {
            string s = $"STAT PACKET\n" +
                       $"Max Health => {maxHealth}\n" +
                       $"Max Mana => {maxMana}\n" +
                       $"Attack Damage => {attackDamage} HP per attack\n" +
                       $"Attack Speed => {attackSpeed} attacks per second\n" +
                       $"Movespeed => {moveSpeed}\n" +
                       $"Armor => {armor}\n" +
                       $"Phys Resist => {physicalResistance * 100}%\n" +
                       $"Negation => {negation}\n" +
                       $"Mag Resist => {magicResistance * 100}%\n" +
                       $"Debuff Resist => {debuffResistance * 100}%\n" +
                       $"Health Regen => {healthRegen} HP per second\n" +
                       $"Mana Regen => {manaRegen} mana per second\n";

            return s;
        }
        
    }
    
}