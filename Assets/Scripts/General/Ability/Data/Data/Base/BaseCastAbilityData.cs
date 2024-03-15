using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Cast")]
    public class BaseCastAbilityData : BaseAbilityData
    {
        [Header("Base Cast Ability Information")] 
        [SerializeField] protected AbilityTargetMethod targetMethod;
        [SerializeField] protected float abilityCastTime;
        [SerializeField] protected float abilityCooldown;
        
        [Header("Level Up Information")]
        [SerializeField] protected float levelUpAbilityCastTime;
        [SerializeField] protected float levelUpAbilityCooldown;

        // Base Cast Ability Information Getters
        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetBaseAbilityCastTime() => abilityCastTime;
        
        public float GetBaseAbilityCooldown() => abilityCooldown;
        
        // Level Up Information Getters
        public float GetLevelUpAbilityCastTime() => levelUpAbilityCastTime;
        public float GetLevelUpAbilityCooldown() => levelUpAbilityCooldown;

        public RealCastAbilityData ToRealCastAbility(int level = 0, RealItemData attachedItem = null)
        {
            RealCastAbilityData real = new RealCastAbilityData(this, abilityCastTime, abilityCooldown, attachedItem);

            real.OnLevelsUp(level);

            return real;
        }
    }
    
    
}