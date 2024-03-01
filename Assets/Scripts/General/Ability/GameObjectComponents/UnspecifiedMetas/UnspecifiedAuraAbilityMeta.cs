using System;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedAuraAbilityMeta : MonoBehaviour, IAuraAbilityMeta
    {
        [SerializeField] private ICharacterEntity owner;
        [SerializeField] private BaseAbilityData data;
        
        public bool OnAttachAura(Character target)
        {
            foreach (BaseEffectData effect in data.GetEffects()) target.AttachEffect(owner, effect);

            return true;
        }

        public bool OnInterruptAura()
        {
            throw new System.NotImplementedException();
        }

        public bool OnCharacterEnterAura(Character target) =>  OnAttachAura(target);

        public bool OnCharacterExitAura(Character target)
        {
            foreach (BaseEffectData effect in data.GetEffects()) target.RemoveEffect(effect);

            return true;
        }
    }
}