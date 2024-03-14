using UnityEngine;

namespace AutoChessRPG
{
    public class BPointTargeted : BLifeTime
    {
        protected Vector3 targetPos;

        protected void Initialize(Vector3 _targetPos)
        {
            targetPos = _targetPos;

            base.Initialize();
        }
    }
}
