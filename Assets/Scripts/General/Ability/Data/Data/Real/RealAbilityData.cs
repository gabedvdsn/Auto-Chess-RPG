using System.Collections.Generic;

namespace AutoChessRPG
{
    public class RealAbilityData : RealData
    {
        private BaseAbilityData baseData;
        
        private RealItemData attachedItem;

        private Dictionary<Affiliation, RealEffectData[]> effectsOfAbility;

        private CastUsagePreference usePreference;

        private float range;

        public RealAbilityData(BaseAbilityData _baseData, RealItemData _attachedItem = null)
        {
            
            baseData = _baseData;
            power = new RealPowerPacket(baseData.GetPowerPacket());

            range = baseData.GetRange();

            attachedItem = _attachedItem;

            effectsOfAbility = _baseData.EffectsToRealByAffiliation();

            usePreference = CastUsagePreference.Immediately;
        }
        
        public virtual void Execute() { }

        public override bool OnLevelUp()
        {
            if (!power.LevelUp()) return false;

            range += baseData.GetLevelUpRange();

            return true;
        }

        public bool OnLevelsUp(int levels)
        {
            for (int i = 0; i < levels; i++)
            {
                if (!power.LevelUp()) return false;
                
                range += baseData.GetLevelUpRange();
            }

            return true;
        }

        public BaseAbilityData GetBaseData() => baseData;

        public RealPowerPacket GetPowerPacket() => power;

        public BasePowerPacket GetBasePowerPacket() => baseData.GetPowerPacket();

        public RealEffectData[] GetEffects(Affiliation affiliation) => effectsOfAbility[affiliation];

        public RealEffectData[] GetAllEffects()
        {
            var realEffects = new List<RealEffectData>();
            foreach (Affiliation affiliation in effectsOfAbility.Keys)
            {
                foreach (RealEffectData effect in effectsOfAbility[affiliation])
                {
                    realEffects.Add(effect);
                }
            }

            return realEffects.ToArray();
        }

        public void SendAttachedItem(RealItemData item) => attachedItem = item;
        public bool IsAttachedToItem() => attachedItem is not null;

        public RealItemData GetAttachmentItem() => attachedItem;

        public CastUsagePreference GetUsePreference() => usePreference;

        public virtual float GetCooldown() => -1f;
    }
}
