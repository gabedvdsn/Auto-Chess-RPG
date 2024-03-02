using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AutoChessRPG
{
    public class RealProjectileCastAbility : UnspecifiedCastAbilityMeta
    {
        // This ability will cast a projectile
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected float projectileSpeed;

        protected Action<EncounterAutoCharacterController> onProjectileHitAction;

        protected virtual void ShootProjectile(GameObject target)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<BFollowAndCollideWithController>().Initialize(target, projectileSpeed, onProjectileHitAction);
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