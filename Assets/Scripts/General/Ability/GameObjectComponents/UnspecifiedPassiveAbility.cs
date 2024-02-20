using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class UnspecifiedPassiveAbility : MonoBehaviour, IPassiveAbilityMeta
    {
        private Character owner;
        private AbilityData data;
        
        public bool OnAttachPassive()
        {
            owner.AttachEffect(owner, data.GetEff)
        }

        public bool OnInterruptPassive()
        {
            throw new System.NotImplementedException();
        }

        public bool OnRemovePassive()
        {
            throw new System.NotImplementedException();
        }
    }
}