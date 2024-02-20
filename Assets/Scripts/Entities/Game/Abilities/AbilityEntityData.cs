using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Ability/Ability Base", fileName = "Ability")]
    public class AbilityEntityData : EntityBaseData
    {
        [Header("Ability Data")] 
        [SerializeField] private Rarity abilityRarity;

    }
}
