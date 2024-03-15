using System;
using System.Linq;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedAuraAbilityMeta : UnspecifiedAbilityMeta, IAuraAbilityMeta
    {
        private RealAbilityData data;

        protected override bool ExecuteAgainstTarget(TargetingPacket target)
        {
            if (!base.ExecuteAgainstTarget(target)) return false;

            if (!target.TryGetControllerFromGameObject(out EncounterAutoCharacterController controller)) return false;

            return OnAttachAura(controller);
        }
        
        public override RealAbilityData GetRealData() => data;

        public bool SendRealAbilityData(RealAbilityData _data)
        {
            if (data is not null) return false;

            data = _data;

            return true;
        }

        public bool ApplyAbilityEffects(EncounterAutoCharacterController target)
        {
            return OnAttachAura(target);
        }
        
        public bool OnAttachAura(EncounterAutoCharacterController target)
        {
            return data.GetAllEffects().All(effect => !target.AttachEffect(owner, effect));

        }

        public bool OnRemoveAura(EncounterAutoCharacterController target)
        {
            return data.GetAllEffects().All(target.RemoveEffect);

        }

        public bool OnInterruptAura()
        {
            throw new System.NotImplementedException();
        }

        public bool OnCharacterEnterAura(EncounterAutoCharacterController target)
        {
            if (target.GetAffiliation() != data.GetBaseData().GetTargetableAffiliation()) return false;

            return OnAttachAura(target);
        }

        public bool OnCharacterExitAura(EncounterAutoCharacterController target)
        {
            if (target.GetAffiliation() != data.GetBaseData().GetTargetableAffiliation()) return false;

            return OnRemoveAura(target);
        }
    }
}