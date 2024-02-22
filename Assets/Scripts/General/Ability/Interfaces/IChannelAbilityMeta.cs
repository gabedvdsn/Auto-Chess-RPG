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
        
        public bool OnChannelStart(AbilityTargetPacket target);  // ability is called, do cast time

        public bool OnChannelBeginPerforming(AbilityTargetPacket target);  // ability call successful, start performance

        public bool OnChannelPerform(AbilityTargetPacket target);  // channel performance logic
        
        public bool OnChannelFinished();  // channel is finished
    }
}