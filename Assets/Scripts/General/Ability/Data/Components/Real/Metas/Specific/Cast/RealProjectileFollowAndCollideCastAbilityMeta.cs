using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AutoChessRPG
{
    public class RealProjectileFollowAndCollideCastAbilityMeta : RealCreateProjectileCastAbilityMeta
    {
        protected virtual void Awake()
        {
            OnCastSucceedAction = InitializeProjectileBehaviour;
        }
        
        protected bool InitializeProjectileBehaviour(TargetingPacket target)
        {
            GameObject projectile = CreateProjectile();

            if (!target.TryGetTargetGameObject(out GameObject obj)) return false;
            
            projectile.GetComponent<BFollowAndCollideWithController>().Initialize(shouldPhase, obj, projectileSpeed, onObjectCollisionAction);

            return projectile;
        }

        public bool Execute(TargetingPacket target) => ExecuteAgainstTarget(target);


        protected override void OnValidate()
        {
            if (!projectilePrefab) return;
            
            if (!projectilePrefab.TryGetComponent(out BFollowAndCollideWithController _))
            {
                throw new Exception("ProjectileCast projectilePrefab requires BFollowAndCollideWithController component");
            }
        }
    }
}