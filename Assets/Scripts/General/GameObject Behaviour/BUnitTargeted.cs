using UnityEngine;

namespace AutoChessRPG
{
    public class BUnitTargeted : BLifeTime
    {
        protected GameObject targetGO;
        protected Transform target;

        protected void Initialize(GameObject _targetGO)
        {
            targetGO = _targetGO;
            target = targetGO.transform;

            base.Initialize();
        }
    }
}
