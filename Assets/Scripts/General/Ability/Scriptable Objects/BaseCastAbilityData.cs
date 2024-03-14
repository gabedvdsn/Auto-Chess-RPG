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

        [Header("Other")] 
        [SerializeField] protected bool hideInUI;

        // Base Cast Ability Information Getters
        public AbilityTargetMethod GetTargetMethod() => targetMethod;
        
        public float GetBaseAbilityCastTime() => abilityCastTime;
        
        public float GetBaseAbilityCooldown() => abilityCooldown;
        
        // Level Up Information Getters
        public float GetLevelUpAbilityCastTime() => levelUpAbilityCastTime;
        public float GetLevelUpAbilityCooldown() => levelUpAbilityCooldown;
        
        // Other
        public bool GetHideInUI() => hideInUI;
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
            if (!base.LevelUp()) return false;

            GetPowerPacket().power = PowerGenerator.GetNewPowerFromDeltaCastTime(GetPowerPacket().power, castTime, castTime + baseData.GetLevelUpAbilityCastTime());
            GetPowerPacket().power = PowerGenerator.GetNewPowerFromDeltaCooldown(GetPowerPacket().power, cooldown, cooldown + baseData.GetLevelUpAbilityCooldown());
            
                
            castTime += baseData.GetLevelUpAbilityCastTime();
            cooldown += baseData.GetLevelUpAbilityCooldown();

            return true;
        }
        public bool GetHideInUI() => baseData.GetHideInUI();

        public float GetRealCastTime() => castTime;

        public float GetRealCooldown() => cooldown;

        public BaseCastAbilityData GetBaseCastData() => baseData;
    }
}