using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public enum Attribute
    {
        Strength,
        Agility,
        Intelligence
    }

    [Serializable]
    public class AttributePacket
    {
        [SerializeField] private int strength;
        [SerializeField] private int agility;
        [SerializeField] private int intelligence;

        public AttributePacket(int _strength, int _agility, int _intelligence)
        {
            strength = _strength;
            agility = _agility;
            intelligence = _intelligence;
        }

        public int Strength() => strength;
        public int Agility() => agility;
        public int Intelligence() => intelligence;

        public void AddStrength(int amount) => strength += amount;
        public void AddAgility(int amount) => agility += amount;
        public void AddIntelligence(int amount) => intelligence += amount;

        public void MergeOtherAttributePacket(AttributePacket other)
        {
            strength += other.Strength();
            agility += other.Agility();
            intelligence += other.Intelligence();
        }

        public int GetAttributeSum() => strength + agility + intelligence;
    }
}
