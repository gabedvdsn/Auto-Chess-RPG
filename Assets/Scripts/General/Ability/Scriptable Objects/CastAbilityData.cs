using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Cast")]
    public class CastAbilityData : AbilityData
    {
        [Header("Cast Ability Information")] 
        [SerializeField] private AbilityTargetMethod targetMethod;
        [SerializeField] private float abilityCastTime;
        [SerializeField] private float abilityCooldown;

        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetAbilityCastTime() => abilityCastTime;
        
        public float GetAbilityCooldown() => abilityCooldown;
        
    }
}