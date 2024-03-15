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
        
        [Header("Specific Ability Information")]
        [SerializeField] protected BaseEffectData[] effectsOfAbility;
        [SerializeField] protected float abilityRange;
        
        [Header("Base Level Up Information")]
        [SerializeField] protected float levelUpAbilityRange;
        
        [Header("Other")]
        [SerializeField] protected bool hideInUI;
        

        // Base Ability Information Getters
        public Affiliation GetTargetableAffiliation() => canTarget;

        public AbilityActivation GetAbilityActivation() => activation;

        public BasePowerPacket GetPowerPacket() => powerPacket;
        
        public BaseEffectData[] GetEffects() => effectsOfAbility;

        public float GetRange() => abilityRange;

        public bool GetHideInUI() => hideInUI;
        
        // Level Up Information Getters
        public float GetLevelUpRange() => levelUpAbilityRange;
    }

    public class RealAbilityData
    {
        private BaseAbilityData baseData;

        private RealPowerPacket power;
        
        private RealItemData attachedItem;

        private RealEffectData[] effectsOfAbility;

        private CastUsagePreference usePreference;

        public RealAbilityData(BaseAbilityData _baseData, RealPowerPacket _power, RealItemData _attachedItem = null)
        {
            baseData = _baseData;
            power = _power;

            attachedItem = _attachedItem;
        }
        
        public virtual void Execute() { }

        public virtual bool LevelUp() => power.LevelUp();

        public BaseAbilityData GetBaseData() => baseData;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();

        public RealEffectData[] GetEffects() => effectsOfAbility;

        public void SetIsAttachedToItem(RealItemData item) => attachedItem = item;
        public bool IsAttachedToItem() => attachedItem is not null;

        public RealItemData GetAttachmentItem() => attachedItem;

        public CastUsagePreference GetUsePreference() => usePreference;

        public virtual float GetCooldown() => -1f;
    }
}
