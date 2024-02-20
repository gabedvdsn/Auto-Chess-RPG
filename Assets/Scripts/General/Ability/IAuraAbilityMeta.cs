using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface IAuraAbilityMeta
    {
        public bool OnAttachAura(Character target);

        public bool OnInterruptAura();

        public bool OnCharacterEnterAura(Character target);

        public bool OnCharacterExitAura(Character target);
    }
}