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

        [Header("Other Information")] 
        [SerializeField] private bool isChargeChannel;

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
        
        // Other Information Getters
        public bool GetIsChargeChannel() => isChargeChannel;

        public RealChannelAbilityData ToRealChannelAbility(int level = 0, RealItemData _attachedItem = null)
        {
            RealChannelAbilityData real = new RealChannelAbilityData(this, abilityCastTime, abilityDuration, abilityCooldown, abilityTickRate, _attachedItem);

            real.OnLevelsUp(level);

            return real;
        }
    }
    
    
}