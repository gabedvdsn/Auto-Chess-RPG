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
        public IEnumerator OnCast(AbilityTargetPacket target);
        
        public void OnCastSucceeded(AbilityTargetPacket target);
    }
}