using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class RealProjectileDirectionalCastAbilityMeta : RealCreateProjectileCastAbilityMeta
    {
        private void Awake()
        {
            OnCastSucceedAction = InitializeProjectileBehaviour;
        }
        
        protected bool InitializeProjectileBehaviour(TargetingPacket target)
        {
            GameObject projectile = CreateProjectile();
            
            if (!target.TryGetTargetPosition(out Vector3 pos)) return false;
            
            projectile.GetComponent<BDirectionalMoveAndCollideWithController>().Initialize(shouldPhase, data.GetBaseData().GetTargetableAffiliation(), pos, projectileSpeed, onObjectCollisionAction);

            return projectile;
        }

        protected override void OnValidate()
        {
            if (projectilePrefab is null) return;
            
            if (!projectilePrefab.TryGetComponent(out BDirectionalMoveAndCollideWithController _))
            {
                throw new Exception("ProjectileCast projectilePrefab requires BDirectionalMoveAndCollideWithController component");
            }
        }
    }
}
