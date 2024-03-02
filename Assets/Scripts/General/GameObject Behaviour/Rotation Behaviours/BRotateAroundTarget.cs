using UnityEngine;

namespace AutoChessRPG.Rotation_Behaviours
{
    public class BRotateAroundTarget : BTargeted
    {
        private float speed;
        
        public void Initialize(GameObject _targetGO, float _speed)
        {
            speed = _speed;

            base.Initialize(_targetGO);
        }

        protected virtual void Update()
        {
            transform.RotateAround(target.position, target.up, speed * Time.deltaTime);
        }
    }
}
