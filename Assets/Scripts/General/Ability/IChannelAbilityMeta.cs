using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface IChannelAbilityMeta
    {
        public bool OnChannelStart(Character target);  // ability is called, do cast time

        public bool OnChannelBeginPerforming(Character target);  // ability call successful, start performance

        public bool OnChannelPerform(Character target);  // channel performance logic

        public bool OnChannelInterrupted();  // interrupted during call (cast time) or performance
        
        public bool OnChannelFinished();  // channel is finished
    }
}