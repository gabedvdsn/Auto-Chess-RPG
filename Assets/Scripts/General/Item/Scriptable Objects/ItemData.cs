using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Item Base", fileName = "Item")]
    public class ItemData : EntityBaseData
    {
        // This data includes items under the following categories: resource, spirit, construction, knowledge
        [Header("Item Data Information")] 
        [SerializeField] private ItemType itemType;
        [SerializeField] private Rarity itemRarity;
        [SerializeField] private AttributePacket attachedAttributes;
        [SerializeField] private StatPacket attachedStats;
        [SerializeField] private AbilityData[] attachedAbilities;

        public ItemType GetItemType() => itemType;
        
        public Rarity GetItemRarity() => itemRarity;

        public AttributePacket GetAttachedAttributes() => attachedAttributes;

        public StatPacket GetAttachedStats() => attachedStats;

        public AbilityData[] GetAttachedAbilities() => attachedAbilities;
    }
}