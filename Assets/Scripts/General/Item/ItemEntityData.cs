using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Item Base", fileName = "Item")]
    public class ItemEntityData : EntityBaseData
    {
        // This data includes items under the following categories: resource, spirit, construction, knowledge
        [Header("Item Data Information")] 
        [SerializeField] private ItemType itemType;
        [SerializeField] private Rarity itemRarity;

        public ItemType GetItemType() => itemType;
        
        public Rarity GetItemRarity() => itemRarity;
    }
}