using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Effect")]
    public class EffectBaseData : EntityBaseData
    {
        [Header("Base Effect Information")] 
        [SerializeField] private float effectDuration;
        [SerializeField] private Dispell dispellRequirement;
        [SerializeField] private bool reverseEffectsAtTermination;

        [Header("Modifier Information")] 
        [SerializeField] private bool applyOnce;
        [SerializeField] private float tickRate;
        [SerializeField] private CharacterModifierTag modifier;
        [SerializeField] private float effectAmount;

        public bool GetApplyOnce() => applyOnce;
        
        public float GetEffectDuration() => effectDuration;
        
        public Dispell GetDispellRequirement() => dispellRequirement;

        public bool GetReverseEffectsAtTermination() => reverseEffectsAtTermination;

        public float GetTickRate() => tickRate;

        public CharacterModifierTag GetModifier() => modifier;

        public float GetEffectAmount() => effectAmount;
    }
}