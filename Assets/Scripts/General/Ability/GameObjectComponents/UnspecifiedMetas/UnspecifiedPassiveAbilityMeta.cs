using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedPassiveAbilityMeta : UnspecifiedAbilityMeta, IPassiveAbilityMeta
    {
        private RealAbilityData data;
        
        public bool OnAttachPassive()
        {
            foreach (RealEffectData effect in data.GetEffects()) owner.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptPassive()
        {
            return OnRemovePassive();
        }

        public bool OnRemovePassive()
        {
            foreach (RealEffectData effect in data.GetEffects()) owner.RemoveEffect(effect);

            return true;
        }
    }
}