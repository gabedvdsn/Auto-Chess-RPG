using UnityEngine;

namespace AutoChessRPG
{
    public class BTargeted : GameObjectBehaviour
    {
        protected GameObject targetGO;
        protected Transform target;

        protected void Initialize(GameObject _targetGO)
        {
            targetGO = _targetGO;
            target = targetGO.transform;

            initialized = true;
        }
    }
}
