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
    }
}
