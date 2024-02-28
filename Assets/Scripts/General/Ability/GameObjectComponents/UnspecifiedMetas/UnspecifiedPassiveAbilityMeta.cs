using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedPassiveAbilityMeta : MonoBehaviour, IPassiveAbilityMeta
    {
        [SerializeField] private ICharacterEntity owner;
        [SerializeField] private AbilityData data;
        
        public bool OnAttachPassive()
        {
            foreach (EffectBaseData effect in data.GetEffects()) owner.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptPassive()
        {
            return OnRemovePassive();
        }

        public bool OnRemovePassive()
        {
            foreach (EffectBaseData effect in data.GetEffects()) owner.RemoveEffect(effect);

            return true;
        }
    }
}