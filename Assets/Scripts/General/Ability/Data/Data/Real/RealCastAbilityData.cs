namespace AutoChessRPG
{
    public class RealCastAbilityData : RealAbilityData
    {
        private BaseCastAbilityData baseData;

        private float castTime;
        private float cooldown;
        
        public RealCastAbilityData(BaseCastAbilityData _baseCastData , float _castTime, float _cooldown, RealItemData _attachedItem = null) : base(_baseCastData, _attachedItem)
        {
            baseData = _baseCastData;

            castTime = _castTime;
            cooldown = _cooldown;

            _attachedItem?.SendCooldown(cooldown);
        }

        public override bool OnLevelUp()
        {
            if (!base.OnLevelUp()) return false;

            GetPowerPacket().power = PowerGenerator.GetNewPowerFromDeltaCastTime(GetPowerPacket().power, castTime, castTime + baseData.GetLevelUpAbilityCastTime());
            GetPowerPacket().power = PowerGenerator.GetNewPowerFromDeltaCooldown(GetPowerPacket().power, cooldown, cooldown + baseData.GetLevelUpAbilityCooldown());
            
                
            castTime += baseData.GetLevelUpAbilityCastTime();
            cooldown += baseData.GetLevelUpAbilityCooldown();

            return true;
        }
        public bool GetHideInUI() => baseData.GetHideInUI();

        public float GetRealCastTime() => castTime;

        public override float GetCooldown() => cooldown;

        public BaseCastAbilityData GetBaseCastData() => baseData;
    }
}
