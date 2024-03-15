using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedPassiveAbilityMeta : UnspecifiedAbilityMeta, IPassiveAbilityMeta
    {
        private RealAbilityData data;

        protected override bool ExecuteAgainstTarget(TargetingPacket target) => OnAttachPassive();
        
        public override RealAbilityData GetRealData() => data;

        public bool SendRealAbilityData(RealAbilityData _data)
        {
            if (data is not null) return false;

            data = _data;

            return true;
        }

        public bool ApplyAbilityEffects(EncounterAutoCharacterController target)
        {
            return OnAttachPassive();
        }
        
        public bool OnAttachPassive()
        {
            foreach (RealEffectData effect in data.GetAllEffects()) owner.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptPassive()
        {
            return OnRemovePassive();
        }

        public bool OnRemovePassive()
        {
            foreach (RealEffectData effect in data.GetAllEffects()) owner.RemoveEffect(effect);

            return true;
        }
    }
}