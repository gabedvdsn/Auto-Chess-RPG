using UnityEngine;

namespace AutoChessRPG
{
    public class BDirectionalMoveToPoint : BPointTargeted
    {
        private float speed;

        protected virtual void Update()
        {
            if (!initialized) return;

            DirectionalMove();
        }

        public void Initialize(Vector3 _targetPos, float _speed)
        {
            speed = _speed;
            
            transform.LookAt((_targetPos - transform.position).normalized);

            base.Initialize(_targetPos);
        }

        private void DirectionalMove()
        {
            if (Vector3.Distance(transform.position, targetPos) <= 0.1f)
            {
                ThisDestroy();
            }
            
            rb.velocity = transform.forward * (speed * Time.deltaTime);
        }
    }
}
