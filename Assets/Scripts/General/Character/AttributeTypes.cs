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

        public int Strength() => strength;
        public int Agility() => agility;
        public int Intelligence() => intelligence;
    }
}
