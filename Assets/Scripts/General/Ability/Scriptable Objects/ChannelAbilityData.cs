using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Channel")]

    public class ChannelAbilityData : AbilityData
    {
        [Header("Channel Ability Information")] 
        [SerializeField] private AbilityTargetMethod targetMethod;
        [SerializeField] private float abilityCastTime;
        [SerializeField] private float abilityDuration;
        [SerializeField] private float abilityCooldown;

        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetAbilityCastTime() => abilityCastTime;

        public float GetAbilityDuration() => abilityDuration;
        
        public float GetAbilityCooldown() => abilityCooldown;
    }
}