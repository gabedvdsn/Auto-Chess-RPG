using System.Collections;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedAbilityMeta : ObservableSubject
    {
        [SerializeField] protected BaseAbilityData baseData;

        protected ICharacterEntity owner;
        
        protected bool attemptingToCast;
        protected bool offCooldown;
        protected bool interrupted;
        
        protected IEnumerator DoAbilityCastTime(float duration)
        {
            attemptingToCast = true;
            
            yield return new WaitForSeconds(duration);

            attemptingToCast = false;
        }

        public bool OnAbilityInterrupt()
        {
            if (!attemptingToCast || !offCooldown) return false;
            
            interrupted = true;
            return true;
        }

        protected IEnumerator DoAbilityCooldown(float duration)
        {
            offCooldown = false;
            
            yield return new WaitForSeconds(duration);
            
            offCooldown = true;
        }
    }
}