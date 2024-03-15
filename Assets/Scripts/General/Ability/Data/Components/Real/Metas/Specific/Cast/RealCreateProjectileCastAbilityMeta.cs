using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public class RealCreateProjectileCastAbilityMeta : UnspecifiedCastAbilityMeta
    {
        // This ability will cast a projectile
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected float projectileSpeed;
        [SerializeField] protected bool shouldPhase = true;
        
        protected Action<EncounterAutoCharacterController> onObjectCollisionAction;
        
        protected GameObject CreateProjectile() => Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        
        protected virtual void OnValidate()
        {

        }
    }
}
