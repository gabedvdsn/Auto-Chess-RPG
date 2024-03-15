using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoChessRPG.Entity.Character;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedChannelAbilityMeta : UnspecifiedAbilityMeta, IChannelAbilityMeta
    {
        private RealChannelAbilityData data;
        [SerializeField] private bool beginCooldownBeforeChannel = true;

        protected bool isChanneling;

        protected override bool ExecuteAgainstTarget(TargetingPacket target)
        {
            if (!base.ExecuteAgainstTarget(target)) return false;

            StartCoroutine(OnChannel(target));

            return true;
        }
        
        protected delegate bool OnChannelFinishedDelegate(TargetingPacket target);
        protected OnChannelFinishedDelegate OnChannelFinishedAction;

        public override RealAbilityData GetRealData() => data;

        public bool ApplyAbilityEffects(EncounterAutoCharacterController target)
        {
            return data.GetAllEffects().All(effect => !target.AttachEffect(owner, effect));
        }

        public bool SendRealChannelAbilityData(RealChannelAbilityData abilityData)
        {
            if (data is not null) return false;

            data = abilityData;

            return true;
        }

        public IEnumerator OnChannel(TargetingPacket target)
        {
            if (!offCooldown) yield break;
            
            yield return StartCoroutine(DoAbilityCastTime(data.GetRealCastTime()));

            if (!interrupted)
            {
                StartCoroutine(OnChannelPerform(target));
                
                if (beginCooldownBeforeChannel) StartCoroutine(DoAbilityCooldown(data.GetRealCooldown()));
            }
            else
            {
                interrupted = false;
            }

        }

        public IEnumerator OnChannelPerform(TargetingPacket target)
        {
            isChanneling = true;
            
            float duration = 0f;
            float ticks = 0f;

            while (duration < data.GetRealDuration())
            {
                if (interrupted)
                {
                    OnChannelFinished(target);
                    yield break;
                }
                
                ticks += Time.deltaTime;

                if (ticks > data.GetRealTickRate())
                {
                    if (!data.GetBaseChannelData().GetIsChargeChannel())
                    {
                        if (target.TryGetControllerFromGameObject(out EncounterAutoCharacterController controller)) ApplyAbilityEffects(controller);  // bug expensive af
                    }

                    ticks = 0f;
                }
                
                duration += Time.deltaTime;

                yield return null;
            }

            OnChannelFinished(target);
        }

        public bool OnChannelFinished(TargetingPacket? target = null)
        {
            if (!beginCooldownBeforeChannel) StartCoroutine(DoAbilityCooldown(data.GetRealCooldown()));
            
            if (interrupted)
            {
                interrupted = false;
                return false;
            }

            if (target is not null) OnChannelFinishedAction(target.Value);
            
            return true;
        }
        
        public bool OnChannelInterrupted()
        {
            if (!isChanneling) return false;
            
            interrupted = true;
            isChanneling = false;

            OnChannelFinished(null);

            return true;
        }
    }
}
