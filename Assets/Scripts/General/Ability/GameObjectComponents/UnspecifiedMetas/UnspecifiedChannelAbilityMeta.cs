using System.Collections;
using System.Collections.Generic;
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
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool Instantiate(RealChannelAbilityData abilityData)
        {
            if (data is not null) return false;

            data = abilityData;

            return true;
        }

        public IEnumerator OnChannel(AbilityTargetPacket target)
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

        public IEnumerator OnChannelPerform(AbilityTargetPacket target)
        {
            float duration = 0f;
            float ticks = 0f;

            while (duration < data.GetRealDuration())
            {
                if (interrupted)
                {
                    interrupted = false;
                    yield break;
                }
                
                ticks += Time.deltaTime;

                if (ticks > data.GetRealTickRate())
                {
                    Debug.Log("Doing stuff to" + target);

                    ticks = 0f;
                }
                
                duration += Time.deltaTime;

                yield return null;
            }
        }

        public bool OnChannelFinished()
        {
            Debug.Log($"Used baseAbility");

            if (!beginCooldownBeforeChannel) StartCoroutine(DoAbilityCooldown(data.GetRealCooldown()));

            return true;
        }
        
        public bool OnChannelInterrupted()
        {
            if (!isChanneling) return false;
            
            interrupted = true;

            return true;
        }
    }
}
