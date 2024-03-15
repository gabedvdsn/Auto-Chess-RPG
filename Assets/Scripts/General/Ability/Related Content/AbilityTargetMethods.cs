using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public enum AbilityTargetMethod
    {
        PointTarget,
        UnitTarget,
        ItemTarget,
        AbilityTarget,
        PointOrUnitTarget,
        Self,
        None
    }

    public struct AbilityTargetPacket
    {
        private Character targetEntity;
        private Transform targetTransform;
        
        public AbilityTargetPacket(Character _targetEntity, Transform _targetTransform)
        {
            targetEntity = _targetEntity;
            targetTransform = _targetTransform;
        }

        public Character GetTargetCharacter() => targetEntity;

        public Transform GetTargetTransform() => targetTransform;

    }
}