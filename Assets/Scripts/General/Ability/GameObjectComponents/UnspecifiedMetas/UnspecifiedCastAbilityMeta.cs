using System;
using System.Collections;
using System.Collections.Generic;
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

        public override void Execute()
        {
            StartCoroutine(OnCast());
        }

        protected delegate void OnCastSucceedDelegate(AbilityTargetPacket targetPacket);
        protected OnCastSucceedDelegate OnCastSucceedAction;

        public override RealAbilityData GetRealData() => data;
        
        public bool SendRealCastAbilityData(RealCastAbilityData abilityData)
        {
            if (data is not null) return false;

            data = abilityData;

            return true;
        }
        
        public IEnumerator OnCast(AbilityTargetPacket target)
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
        
        public void OnCastSucceeded(AbilityTargetPacket target)
        {
            OnCastSucceedAction(target);
        }

        public BaseCastAbilityData GetData() => data.GetBaseCastData();
    }
}
