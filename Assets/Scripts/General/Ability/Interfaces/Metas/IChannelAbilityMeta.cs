using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface IChannelAbilityMeta : IAbilityMeta
    {
        public IEnumerator OnChannel(AbilityTargetPacket target);
        
        public IEnumerator OnChannelPerform(AbilityTargetPacket target);  // channel performance logic
        
        public bool OnChannelFinished();  // channel is finished

        public bool OnChannelInterrupted();
    }
}