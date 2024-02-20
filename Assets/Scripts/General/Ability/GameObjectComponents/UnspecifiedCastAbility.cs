using System.Collections;
using System.Collections.Generic;
using System.Text;
using AutoChessRPG.Entity.Character;
using JetBrains.Annotations;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedCastAbility : MonoBehaviour, ICastAbilityMeta
    {
        private Character owner;
        private AbilityData data;

        private bool attemptingToCast;
        private bool interrupted;
        private bool offCooldown;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public bool OnCastStart()
        {
            // If ability is on cooldown, return false
            if (!offCooldown) return false;

            attemptingToCast = true;

            // Do cast time
            StartCoroutine(DoAbilityCastTime());

            // If interrupted during cast time, reset flag and return false
            if (interrupted)
            {
                interrupted = false;
                return false;
            }
            
            // Cast time succeeded and not interrupted, deploy ability and return true
            OnCastSucceeded();

            return true;
        }

        public bool OnCastInterrupted()
        {
            if (!attemptingToCast) return false;
            
            interrupted = true;
            
            return true;
        }

        public bool OnCastSucceeded()
        {
            Debug.Log($"Used ability");

            StartCoroutine(DoAbilityCooldown());

            return true;
        }

        private IEnumerator DoAbilityCastTime()
        {
            yield return new WaitForSeconds(data.GetAbilityCastTime());

            attemptingToCast = false;
        }

        private IEnumerator DoAbilityCooldown()
        {
            offCooldown = false;
            
            yield return new WaitForSeconds(data.GetAbilityCooldown());
            
            offCooldown = true;
        }
    }
}
