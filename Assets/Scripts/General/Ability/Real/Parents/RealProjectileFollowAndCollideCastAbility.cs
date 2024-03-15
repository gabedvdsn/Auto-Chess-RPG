using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AutoChessRPG
{
    public class RealProjectileFollowAndCollideCastAbility : UnspecifiedCastAbilityMeta
    {
        // This ability will cast a projectile
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected float projectileSpeed;
        [SerializeField] protected bool phase;

        protected Action<EncounterAutoCharacterController> onProjectileHitAction;

        protected virtual void ShootProjectile(GameObject target)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<BFollowAndCollideWithController>().Initialize(phase, target, projectileSpeed, onProjectileHitAction);
        }

        private void OnValidate()
        {
            if (projectilePrefab is null) return;
            
            if (!projectilePrefab.TryGetComponent(out BFollowAndCollideWithController _))
            {
                throw new Exception("ProjectileCast projectilePrefab requires BFollowAndCollideWithController component");
            }
        }
    }
}