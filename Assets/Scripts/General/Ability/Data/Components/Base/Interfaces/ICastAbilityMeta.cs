using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface ICastAbilityMeta : IAbilityMeta
    {
        public IEnumerator OnCast(TargetingPacket target);
        
        public void OnCastSucceeded(TargetingPacket target);
    }
}