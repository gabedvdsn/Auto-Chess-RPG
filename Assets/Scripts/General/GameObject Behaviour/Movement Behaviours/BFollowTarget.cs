using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BFollowTarget : BTargeted
    {
        private float speed;
        private Vector3 direction;

        protected virtual void Update()
        {
            if (!initialized) return;
            
            FollowTarget();
        }
        
        public void Initialize(GameObject _targetGO, float _speed)
        {
            speed = _speed;

            base.Initialize(_targetGO);
            
            InitialFaceTarget();
        }

        protected virtual void InitialFaceTarget()
        {
            transform.LookAt(target);
        }
        
        protected virtual void FollowTarget()
        {
            direction = (target.position - transform.position).normalized;
            
            rb.velocity = direction * speed;
        }
    }
}
