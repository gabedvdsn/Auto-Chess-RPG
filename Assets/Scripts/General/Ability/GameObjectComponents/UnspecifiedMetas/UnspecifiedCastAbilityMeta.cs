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
        [SerializeField] private BaseCastAbilityData data;
        
        public IEnumerator OnCast(AbilityTargetPacket target)
        {
            if (!offCooldown) yield break;
            
            yield return StartCoroutine(DoAbilityCastTime(data.GetAbilityCastTime()));
            
            if (!interrupted)
            {
                OnCastSucceeded(target);
                StartCoroutine(DoAbilityCooldown(data.GetAbilityCooldown()));
            }
            else
            {
                interrupted = false;
            }
        }
        
        public void OnCastSucceeded(AbilityTargetPacket target)
        {
            Debug.Log($"Used baseAbility against {target}");
        }

        public BaseCastAbilityData GetData() => data;
    }
}
