using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public interface ICastAbilityMeta
    {
        public bool OnCastStart();

        public bool OnCastInterrupted();

        public bool OnCastSucceeded();
    }
}