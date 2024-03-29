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
    public class BaseEffectData : EntityBaseData
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
        
        [Header("Level Up Information")]
        [SerializeField] private float levelUpEffectDuration;
        [SerializeField] private float levelUpEffectAmount;
        [SerializeField] private float levelUpTickRate;

        // Base Effect Information Getters
        public float GetDuration() => effectDuration;
        
        public Dispell GetDispellRequirement() => dispellRequirement;

        public bool GetReverseEffectsAtTermination() => reverseEffectsAtTermination;
        
        // Modifier Information Getters
        public bool GetApplyOnce() => applyOnce;

        public float GetTickRate() => tickRate;

        public CharacterModifierTag GetModifier() => modifier;

        public float GetEffectAmount() => effectAmount;
        
        // Level Up Information Getters
        public float GetLevelUpEffectDuration() => levelUpEffectDuration;
        public float GetLevelUpEffectAmount() => levelUpEffectAmount;
        public float GetLevelUpEffectTickRate() => levelUpTickRate;

        public RealEffectData ToRealEffect(int level = 0)
        {
            RealEffectData real = new RealEffectData(this, tickRate, effectDuration, effectAmount);

            real.OnLevelsUp(level);

            return real;
        }
    }

    public class RealEffectData : RealData
    {
        private BaseEffectData baseData;

        public float tickRate;
        private float effectDuration;
        private float effectAmount;

        public RealEffectData(BaseEffectData _baseData, float _tickRate, float _effectDuration, float _effectAmount)
        {
            baseData = _baseData;
            tickRate = _tickRate;
            effectDuration = _effectDuration;
            effectAmount = _effectAmount;
        }

        public override bool OnLevelUp()
        {
            if (!power.LevelUp()) return false;
            
            tickRate += baseData.GetLevelUpEffectTickRate();
            effectDuration += baseData.GetLevelUpEffectDuration();
            effectAmount += baseData.GetLevelUpEffectAmount();

            return true;
        }

        public bool OnLevelsUp(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                if (!power.LevelUp()) return false;
                
                tickRate += baseData.GetLevelUpEffectTickRate();
                effectDuration += baseData.GetLevelUpEffectDuration();
                effectAmount += baseData.GetLevelUpEffectAmount();
            }

            return true;
        }

        public BaseEffectData GetBaseData() => baseData;

        public float GetTickRate() => tickRate;

        public float GetDuration() => effectDuration;

        public float GetAmount() => effectAmount;
    }
}