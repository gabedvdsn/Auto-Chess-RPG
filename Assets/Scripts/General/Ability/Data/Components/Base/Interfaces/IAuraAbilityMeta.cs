using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface IAuraAbilityMeta : IAbilityMeta
    {
        public bool OnAttachAura(EncounterAutoCharacterController target);

        public bool OnInterruptAura();

        public bool OnCharacterEnterAura(EncounterAutoCharacterController target);

        public bool OnCharacterExitAura(EncounterAutoCharacterController target);
    }
}