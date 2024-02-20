using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Item Base", fileName = "Item")]
    public class ItemEntityData : EntityBaseData
    {
        [Header("Item Data Information")] 
        [SerializeField] private Rarity itemRarity;
        [SerializeField] private ItemActivation itemActivation;
    }
}
