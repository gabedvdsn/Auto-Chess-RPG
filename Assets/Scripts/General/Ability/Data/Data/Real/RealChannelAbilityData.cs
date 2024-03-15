namespace AutoChessRPG
{
    public class RealChannelAbilityData : RealAbilityData
    {
        private BaseChannelAbilityData baseData;
        
        private float castTime;
        private float duration;
        private float cooldown;
        private float tickRate;

        public RealChannelAbilityData(BaseChannelAbilityData _baseData , float _castTime, float _duration, float _cooldown, float _tickRate, RealItemData _attachedItem = null) : base(_baseData, _attachedItem)
        {
            baseData = _baseData;
            
            castTime = _castTime;
            duration = _duration;
            cooldown = _cooldown;
            tickRate = _tickRate;
            
            _attachedItem?.SendCooldown(cooldown);
        }

        public override bool OnLevelUp()
        {
            if (!base.OnLevelUp()) return false;

            castTime += baseData.GetLevelUpAbilityCastTime();
            duration += baseData.GetLevelUpAbilityDuration();
            cooldown += baseData.GetLevelUpAbilityCooldown();

            return true;
        }

        // Setters
        
        
        // Getters
        public BaseChannelAbilityData GetBaseChannelData() => baseData;
        
        public float GetRealCastTime() => castTime;
        
        public float GetRealDuration() => duration;
        
        public float GetRealCooldown() => cooldown;
        
        public float GetRealTickRate() => tickRate;
    }
}
