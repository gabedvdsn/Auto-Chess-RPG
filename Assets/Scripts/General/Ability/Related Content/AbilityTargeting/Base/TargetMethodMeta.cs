using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AutoChessRPG
{
    public class TargetMethodMeta
    {
        protected bool targeting;

        public virtual bool ToggleTargeting()
        {
            targeting = !targeting;

            return MouseCursorManager.Instance.SetCursorMode(MouseCursorMode.Regular);
        }
    }

    public class TargetMethodPointTargetMeta : TargetMethodMeta
    {
        
    }
    
    public class TargetMethodUnitTargetMeta : TargetMethodMeta
    {
        
    }
    
    public class TargetMethodPointOrUnitTargetMeta : TargetMethodMeta
    {
        
    }
    
    public class TargetMethodItemTargetMeta : TargetMethodMeta
    {
        
    }
    
    public class TargetMethodAbilityTargetMeta : TargetMethodMeta
    {
        
    }

    public struct TargetingPacket
    {
        private GameObject targetGO;
        private Vector3? targetPos;

        public TargetingPacket(GameObject _targetGO = null, Vector3? _targetPos = null)
        {
            targetGO = _targetGO;
            targetPos = _targetPos;
        }
        
        public bool TryGetControllerFromGameObject(out EncounterAutoCharacterController controller)
        {
            if (targetGO is not null) return targetGO.TryGetComponent(out controller);
            
            controller = null;
            return false;

        }
        
        public bool TryGetTargetGameObject(out GameObject obj)
        {
            if (targetGO is not null)
            {
                obj = targetGO;
                return true;
            }

            obj = null;
            return false;
        }

        public Transform GetTargetGOTransform()
        {
            return targetGO != null ? targetGO.transform : null;

        }

        public bool TryGetTargetPosition(out Vector3 pos)
        {
            if (targetPos is not null)
            {
                pos = targetPos.Value;
                return true;
            }

            pos = Vector3.zero;
            return false;
        }

    }
}
