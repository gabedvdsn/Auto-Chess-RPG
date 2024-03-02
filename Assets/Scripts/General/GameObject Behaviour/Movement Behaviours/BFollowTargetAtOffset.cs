using UnityEngine;

namespace AutoChessRPG
{
    public class BFollowTargetAtOffset : BFollowTarget
    {
        private float offset;

        public void Initialize(GameObject _targetGO, float _speed, float _offset)
        {
            offset = _offset;
            
            base.Initialize(_targetGO, _speed);
        }

        protected override void FollowTarget()
        {
            if (Vector3.Distance(target.position, transform.position) < offset) return;
            
            base.FollowTarget();
        }
    }
}
