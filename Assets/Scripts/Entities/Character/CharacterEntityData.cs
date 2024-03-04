using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Character Entity/Character Base", fileName = "Character")]
    public class CharacterEntityData : EntityBaseData
    {
        [Header("Character Information")]
        [SerializeField] private bool isHero;
        [SerializeField] private Race characterRace;
        [SerializeField] private Affinity[] characterAffinities;
        [SerializeField] private AttributePacket characterAttributes;

        [Header("Physical Information")] 
        [SerializeField] [Range(0, 90f)] private float allowableActionRange = 90f;

        private void OnValidate()
        {
            
        }

        public bool GetIsHero() => isHero;
        
        public Race GetCharacterRace() => characterRace;

        public Affinity[] GetCharacterAffinities() => characterAffinities;
        
        public AttributePacket GetAttributes() => characterAttributes;

        public float GetAllowableActionRange() => allowableActionRange;
    }

    [Serializable]
    public struct CharacterAttributes
    {
        [SerializeField] private int strength;
        [SerializeField] private int agility;
        [SerializeField] private int intelligence;
    }
}
