using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedPassiveAbilityMeta : MonoBehaviour, IPassiveAbilityMeta
    {
        [SerializeField] private ICharacterEntity owner;
        [SerializeField] private BaseAbilityData data;
        
        public bool OnAttachPassive()
        {
            foreach (BaseEffectData effect in data.GetEffects()) owner.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptPassive()
        {
            return OnRemovePassive();
        }

        public bool OnRemovePassive()
        {
            foreach (BaseEffectData effect in data.GetEffects()) owner.RemoveEffect(effect);

            return true;
        }
    }
}