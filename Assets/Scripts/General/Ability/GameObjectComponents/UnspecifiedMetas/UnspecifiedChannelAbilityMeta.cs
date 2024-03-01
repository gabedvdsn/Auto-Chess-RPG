using System.Collections;
using System.Collections.Generic;
using System.Text;
using AutoChessRPG.Entity.Character;
using JetBrains.Annotations;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedChannelAbilityMeta : UnspecifiedAbilityMeta, IChannelAbilityMeta
    {
        [SerializeField] private ChannelBaseAbilityData data;

        protected bool isChanneling;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator OnChannel(AbilityTargetPacket target)
        {
            if (!offCooldown) yield break;
            
            yield return StartCoroutine(DoAbilityCastTime(data.GetAbilityCastTime()));

            if (!interrupted)
            {
                OnChannelBeginPerforming(target);
                StartCoroutine(DoAbilityCooldown(data.GetAbilityCooldown()));  // start cooldown as soon as the baseAbility is used
            }
            else
            {
                interrupted = false;
            }

        }

        public bool OnChannelStart(AbilityTargetPacket target)
        {
            // If baseAbility is on cooldown, return false
            if (!offCooldown) return false;
            
            // Do cast time
            StartCoroutine(DoAbilityCastTime(data.GetAbilityCastTime()));

            // If interrupted during cast time, reset flag and return false
            if (interrupted)
            {
                interrupted = false;
                return false;
            }
            
            // Cast time succeeded and not interrupted, deploy baseAbility and return true
            OnChannelBeginPerforming(target);

            return true;
        }

        public bool OnChannelBeginPerforming(AbilityTargetPacket target)
        {
            isChanneling = true;
            
            // Check if interrupted
            if (interrupted)
            {
                OnChannelFinished();
                return false;
            }

            OnChannelPerform(target);
            return true;
        }

        public bool OnChannelPerform(AbilityTargetPacket target)
        {
            // put channel performance logic here
            Debug.Log("Channeling against " + target);

            return !interrupted;
        }

        public bool OnChannelFinished()
        {
            Debug.Log($"Used baseAbility");

            StartCoroutine(DoAbilityCooldown(data.GetAbilityCooldown()));

            return true;
        }
    }
}
