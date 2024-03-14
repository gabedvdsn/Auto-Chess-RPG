using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BDirectionalMoveAndCollideWithController : BDirectionalMoveToPoint
    {
        protected bool phase;
        
        private Affiliation targetAff;
        private Action<EncounterAutoCharacterController> onCollision;

        public void Initialize(bool _phase, Affiliation _targetAffiliation, Vector3 _targetPos, float _speed, Action<EncounterAutoCharacterController> _onCollision)
        {
            phase = _phase;
            
            targetAff = _targetAffiliation;
            
            onCollision = _onCollision;

            base.Initialize(_targetPos, _speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EncounterAutoCharacterController controller))
            {
                
            }
        }
    }
}
