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
    }
    
    public class RealCastAbilityData : RealAbilityData
    {
        private BaseCastAbilityData baseData;

        private float castTime;
        private float cooldown;

        public RealCastAbilityData(BaseCastAbilityData _baseCastData, RealPowerPacket _power, float _castTime, float _cooldown) : base(_baseCastData, _power)
        {
            baseData = _baseCastData;

            castTime = _castTime;
            cooldown = _cooldown;
        }

        public override bool LevelUp()
        {
            // Implemented abilities will also need to implement their own level up functionality
            
            if (!base.LevelUp()) return false;

            castTime += baseData.GetLevelUpAbilityCastTime();
            cooldown += baseData.GetLevelUpAbilityCooldown();

            return true;
        }

        public BaseCastAbilityData GetBaseCastData() => baseData;
    }
}