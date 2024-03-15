using System.Collections;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedAbilityMeta : ObservableSubject
    {
        protected ICharacterEntity owner;
        
        protected bool attemptingToCast;
        protected bool offCooldown;
        protected bool interrupted;
        
        protected virtual bool ExecuteAgainstTarget(TargetingPacket target) => true;
        
        public virtual RealAbilityData GetRealData() => null;

        public bool IsOffCooldown() => offCooldown;

        public bool IsInterrupted() => interrupted;

        public bool IsAttemptingToCast() => attemptingToCast;
        
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