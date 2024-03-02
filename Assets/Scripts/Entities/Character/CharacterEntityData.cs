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
        [SerializeField] private Race characterRace;
        [FormerlySerializedAs("characterAffinity")] [SerializeField] private Affinity[] characterAffinities;
        [SerializeField] private AttributePacket characterAttributes;

        [Header("Physical Information")] 
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] [Range(0, 90f)] private float allowableActionRange = 90f;

        private void OnValidate()
        {
            
        }
        
        public Race GetCharacterRace() => characterRace;

        public Affinity[] GetCharacterAffinities() => characterAffinities;
        
        public AttributePacket GetAttributes() => characterAttributes;

        public float GetMovementSpeed() => movementSpeed;

        public float GetRotationSpeed() => rotationSpeed;

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
