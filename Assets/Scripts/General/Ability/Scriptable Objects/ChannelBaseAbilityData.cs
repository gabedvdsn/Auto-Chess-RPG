using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Channel")]

    public class ChannelBaseAbilityData : BaseAbilityData
    {
        [Header("Channel Ability Information")] 
        [SerializeField] private AbilityTargetMethod targetMethod;
        [SerializeField] private float abilityCastTime;
        [SerializeField] private float abilityDuration;
        [SerializeField] private float abilityCooldown;
        
        [Header("Level Up Information")]
        [SerializeField] private float levelUpAbilityCastTime;
        [SerializeField] private float levelUpAbilityDuration;
        [SerializeField] private float levelUpAbilityCooldown;

        // Base Channel Ability Information Getters
        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetBaseAbilityCastTime() => abilityCastTime;

        public float GetBaseAbilityDuration() => abilityDuration;
        
        public float GetBaseAbilityCooldown() => abilityCooldown;
        
        // Level Up Information Getters
        public float GetLevelUpAbilityCastTime() => levelUpAbilityCastTime;

        public float GetLevelUpAbilityDuration() => levelUpAbilityDuration;
        
        public float GetLevelUpAbilityCooldown() => levelUpAbilityCooldown;
    }
    
    public class RealChannelAbilityData : RealAbilityData
    {
        private BaseCastAbilityData baseCastData;

        private RealPowerPacket power;

        private float castTime;
        private float duration;
        private float cooldown;

        public RealChannelAbilityData(BaseCastAbilityData _baseCastData, RealPowerPacket _power, float _castTime, float _duration, float _cooldown) : base(_baseCastData)
        {
            baseCastData = _baseCastData;

            power = _power;

            castTime = _castTime;
            duration = _duration;
            cooldown = _cooldown;
        }

        public bool LevelUp

        // Setters
        
        
        // Getters
        public BaseCastAbilityData GetBaseCastData() => baseCastData;
        
        public float GetRealCastTime() => castTime;
        
        public float GetRealDuration() => duration;
        
        public float GetRealCooldown() => cooldown;
    }
}