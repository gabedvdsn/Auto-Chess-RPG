using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public class AbilityData : EntityBaseData
    {
        [Header("Ability Information")] 
        [SerializeField] private string abilityName;
        [SerializeField] private AbilityActivation abilityActivation;
        [SerializeField] private float abilityCastTime;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private EffectBaseData[] effects;

        public string GetAbilityName() => abilityName;
        public AbilityActivation GetAbilityActivation() => abilityActivation;
        public float GetAbilityCastTime() => abilityCastTime;
        public float GetAbilityCooldown() => abilityCooldown;
        public EffectBaseData[] GetEffects() => effects;
    }
}
