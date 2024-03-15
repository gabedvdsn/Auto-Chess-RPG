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
        public IEnumerator OnChannel(TargetingPacket target);
        
        public IEnumerator OnChannelPerform(TargetingPacket target);  // channel performance logic
        
        public bool OnChannelFinished(TargetingPacket? target);  // channel is finished

        public bool OnChannelInterrupted();
    }
}