using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG
{
    public class RealSingleObjectChannelAbility : UnspecifiedChannelAbilityMeta
    {
        [SerializeField] protected GameObject objectPrefab;
        [SerializeField] protected float objectSpeed;
        [SerializeField] protected bool shouldPhase;
        
        protected Action<EncounterAutoCharacterController> onObjectCollisionAction;
        
        protected GameObject CreateObject() => Instantiate(objectPrefab, transform.position, Quaternion.identity);
        
        protected virtual void OnValidate()
        {

        }
    }
}
