using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Attribute Item Base", fileName = "AttributeItem")]
    public class AttributeItemData : ItemEntityData
    {
        [Header("Attribute Item Information")] 
        [SerializeField] private StatPacket attachedStats;
        [SerializeField] private AttributePacket attachedAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public AttributePacket GetAttachedAttributes() => attachedAttributes;
    }
}
