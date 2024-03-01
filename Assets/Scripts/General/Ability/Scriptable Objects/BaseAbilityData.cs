using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Base")]
    public class BaseAbilityData : EntityBaseData
    {
        [Header("Base Ability Information")] 
        [SerializeField] protected Affiliation canTarget;
        [SerializeField] protected AbilityActivation activation;
        [SerializeField] protected BasePowerPacket powerPacket;
        [SerializeField] protected BaseEffectData[] effectsOfAbility;

        // Base Ability Information Getters
        public Affiliation GetTargetableAffiliation() => canTarget;

        public AbilityActivation GetAbilityActivation() => activation;

        public BasePowerPacket GetPowerPacket() => powerPacket;
        
        public BaseEffectData[] GetEffects() => effectsOfAbility;
    }

    public class RealAbilityData
    {
        private BaseAbilityData baseData;

        private RealPowerPacket power;

        public RealAbilityData(BaseAbilityData _baseData, RealPowerPacket _power)
        {
            baseData = _baseData;
            power = _power;
        }

        public virtual bool LevelUp() => power.LevelUp();

        public BaseAbilityData GetBaseData() => baseData;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();
    }
}
