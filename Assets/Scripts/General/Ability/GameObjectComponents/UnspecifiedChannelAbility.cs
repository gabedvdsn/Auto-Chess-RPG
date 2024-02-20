using System.Collections;
using System.Collections.Generic;
using System.Text;
using AutoChessRPG.Entity.Character;
using JetBrains.Annotations;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedChannelAbility : MonoBehaviour, IChannelAbilityMeta
    {
        private Character owner;
        private AbilityData data;

        protected bool attemptingToChannel;
        protected bool isChanneling;
        protected bool interrupted;
        protected bool offCooldown;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator DoAbilityCastTime()
        {
            yield return new WaitForSeconds(data.GetAbilityCastTime());

            attemptingToChannel = false;
        }

        private IEnumerator DoAbilityCooldown()
        {
            offCooldown = false;
            
            yield return new WaitForSeconds(data.GetAbilityCooldown());
            
            offCooldown = true;
        }

        public bool OnChannelStart(Character target)
        {
            // If ability is on cooldown, return false
            if (!offCooldown) return false;

            attemptingToChannel = true;

            // Do cast time
            StartCoroutine(DoAbilityCastTime());

            // If interrupted during cast time, reset flag and return false
            if (interrupted)
            {
                interrupted = false;
                return false;
            }
            
            // Cast time succeeded and not interrupted, deploy ability and return true
            OnChannelBeginPerforming(target);

            return true;
        }

        public bool OnChannelBeginPerforming(Character target)
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

        public bool OnChannelPerform(Character target)
        {
            // put channel performance logic here
            Debug.Log("Channeling against " + target);

            return !interrupted;
        }

        public bool OnChannelInterrupted()
        {
            if (!attemptingToChannel && !isChanneling) return false;
            
            interrupted = true;
            
            return true;
        }
        
        public bool OnChannelFinished()
        {
            Debug.Log($"Used ability");

            StartCoroutine(DoAbilityCooldown());

            return true;
        }
    }
}
