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

        [Header("Physical Information")] 
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float turnSpeed = 5f;
        [SerializeField] [Range(0, 90f)] private float allowableActionRange = 90f;

        private void OnValidate()
        {
            
        }
        
        public Race GetCharacterRace() => characterRace;

        public Affinity GetCharacterAffinity() => characterAffinity;
        
        public AttributePacket GetAttributes() => characterAttributes;

        public float GetMovementSpeed() => movementSpeed;

        public float GetTurnSpeed() => turnSpeed;

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
