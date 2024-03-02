using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BFollowAndCollideWithController : BFollowTarget
    {
        private Action<EncounterAutoCharacterController> onCollision;

        public void Initialize(GameObject _targetGO, float _speed, Action<EncounterAutoCharacterController> _onCollision)
        {
            Debug.Log($"Projectile initialized with target {_targetGO}");
            onCollision = _onCollision;
            
            base.Initialize(_targetGO, _speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject != targetGO) return;

            onCollision(other.gameObject.GetComponent<EncounterAutoCharacterController>());

            ThisDestroy();
        }

    }
}
