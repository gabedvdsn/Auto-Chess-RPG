using System;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedAuraAbilityMeta : UnspecifiedAbilityMeta, IAuraAbilityMeta
    {
        private RealAbilityData data;

        public override RealAbilityData GetRealData() => data;

        public bool SendRealAbilityData(RealAbilityData _data)
        {
            if (data is not null) return false;

            data = _data;

            return true;
        }
        
        public bool OnAttachAura(Character target)
        {
            foreach (RealEffectData effect in data.GetEffects()) target.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptAura()
        {
            throw new System.NotImplementedException();
        }

        public bool OnCharacterEnterAura(Character target) =>  OnAttachAura(target);

        public bool OnCharacterExitAura(Character target)
        {
            foreach (RealEffectData effect in data.GetEffects()) target.RemoveEffect(effect);

            return true;
        }
    }
}