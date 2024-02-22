using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Base")]
    public class AbilityData : EntityBaseData
    {
        [Header("Base Ability Information")] 
        [SerializeField] private Affiliation[] canTarget;
        [SerializeField] private EffectBaseData[] effectsOfAbility;

        public Affiliation[] GetTargetableAffiliations() => canTarget;
        public string GetAbilityName() => GetEntityName();
        
        public EffectBaseData[] GetEffects() => effectsOfAbility;
    }
}
