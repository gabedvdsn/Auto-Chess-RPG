using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG.Entity.Character
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Character Entity/Character Base", fileName = "Character")]
    public class CharacterEntityData : EntityBaseData
    {
        [Header("Character Information")] 
        [SerializeField] private Race characterRace;
        [SerializeField] private Affinity characterAffinity;
        [SerializeField] private AttributePacket characterAttributes;

        private void OnValidate()
        {
            
        }
        
        public Race GetCharacterRace() => characterRace;

        public Affinity GetCharacterAffinity() => characterAffinity;
        
        public AttributePacket GetAttributes() => characterAttributes;
    }

    [Serializable]
    public struct CharacterAttributes
    {
        [SerializeField] private int strength;
        [SerializeField] private int agility;
        [SerializeField] private int intelligence;
    }

    [Serializable]
    public readonly struct CharacterStats
    {
        public readonly float health;
        public readonly float manaPool;
        public readonly float moveSpeed;
        public readonly float attackSpeed;
    }
}
