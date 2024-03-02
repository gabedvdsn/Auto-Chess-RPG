using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Channel")]

    public class BaseChannelAbilityData : BaseAbilityData
    {
        [Header("Channel Ability Information")] 
        [SerializeField] private AbilityTargetMethod targetMethod;
        [SerializeField] private float abilityCastTime;
        [SerializeField] private float abilityDuration;
        [SerializeField] private float abilityCooldown;
        [SerializeField] private float abilityTickRate;
        
        [Header("Level Up Information")]
        [SerializeField] private float levelUpAbilityCastTime;
        [SerializeField] private float levelUpAbilityDuration;
        [SerializeField] private float levelUpAbilityCooldown;

        // Base Channel Ability Information Getters
        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetBaseAbilityCastTime() => abilityCastTime;

        public float GetBaseAbilityDuration() => abilityDuration;
        
        public float GetBaseAbilityCooldown() => abilityCooldown;

        public float GetBaseAbilityTickRate() => abilityTickRate;
        
        // Level Up Information Getters
        public float GetLevelUpAbilityCastTime() => levelUpAbilityCastTime;

        public float GetLevelUpAbilityDuration() => levelUpAbilityDuration;
        
        public float GetLevelUpAbilityCooldown() => levelUpAbilityCooldown;
    }
    
    public class RealChannelAbilityData : RealAbilityData
    {
        private BaseChannelAbilityData baseData;

        private RealPowerPacket power;

        private float castTime;
        private float duration;
        private float cooldown;
        private float tickRate;

        public RealChannelAbilityData(BaseChannelAbilityData _baseData, RealPowerPacket _power, float _castTime, float _duration, float _cooldown, float _tickRate) : base(_baseData, _power)
        {
            baseData = _baseData;

            power = _power;

            castTime = _castTime;
            duration = _duration;
            cooldown = _cooldown;
            tickRate = _tickRate;
        }

        public override bool LevelUp()
        {
            if (!base.LevelUp()) return false;

            castTime += baseData.GetLevelUpAbilityCastTime();
            duration += baseData.GetLevelUpAbilityDuration();
            cooldown += baseData.GetLevelUpAbilityCooldown();

            return true;
        }

        // Setters
        
        
        // Getters
        public BaseChannelAbilityData GetBaseCastData() => baseData;
        
        public float GetRealCastTime() => castTime;
        
        public float GetRealDuration() => duration;
        
        public float GetRealCooldown() => cooldown;
        
        public float GetRealTickRate() => tickRate;
    }
}