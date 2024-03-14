using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BFollowAndCollideWithController : BFollowTarget
    {
        protected bool phase;
        private Action<EncounterAutoCharacterController> onCollision;

        public void Initialize(bool _phase, GameObject _targetGO, float _speed, Action<EncounterAutoCharacterController> _onCollision)
        {
            Debug.Log($"Projectile initialized with target {_targetGO}");
            phase = _phase;
            onCollision = _onCollision;
            
            base.Initialize(_targetGO, _speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != targetGO)
            {
                if (phase) return;
                ThisDestroy();
            }
            
            Debug.Log($"{gameObject} collided with {other.gameObject.GetComponent<EncounterAutoCharacterController>().GetAffiliation()} {other.gameObject}");

            onCollision(other.gameObject.GetComponent<EncounterAutoCharacterController>());

            ThisDestroy();
        }

        private void OnCollisionEnter(Collision _)
        {
            if (phase) return;
            
            ThisDestroy();
        }

    }
}
