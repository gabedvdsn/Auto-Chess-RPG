using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoChessRPG.Entity.Character;
using JetBrains.Annotations;
using UnityEditor.Build;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedCastAbilityMeta : UnspecifiedAbilityMeta, ICastAbilityMeta
    {
        protected RealCastAbilityData data;

        protected override bool ExecuteAgainstTarget(TargetingPacket target)
        {
            if (!base.ExecuteAgainstTarget(target)) return false;
            
            StartCoroutine(OnCast(target));

            return true;
        }

        protected delegate bool OnCastSucceedDelegate(TargetingPacket target);
        protected OnCastSucceedDelegate OnCastSucceedAction;

        public override RealAbilityData GetRealData() => data;
        
        public bool SendRealCastAbilityData(RealCastAbilityData abilityData)
        {
            if (data is not null) return false;

            data = abilityData;
            
            return true;
        }

        public bool ApplyAbilityEffects(EncounterAutoCharacterController target)
        {
            return data.GetAllEffects().All(effect => !target.AttachEffect(owner, effect));
        }
        
        public IEnumerator OnCast(TargetingPacket target)
        {
            if (!offCooldown) yield break;
            
            yield return StartCoroutine(DoAbilityCastTime(data.GetRealCastTime()));
            
            if (!interrupted)
            {
                OnCastSucceeded(target);
                StartCoroutine(DoAbilityCooldown(data.GetCooldown()));
            }
            else
            {
                interrupted = false;
            }
        }
        
        public void OnCastSucceeded(TargetingPacket target)
        {
            OnCastSucceedAction(target);
        }

        public BaseCastAbilityData GetData() => data.GetBaseCastData();
    }
}
