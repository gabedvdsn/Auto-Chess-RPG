using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class GameObjectBehaviour : MonoBehaviour
    {
        protected bool initialized;
        protected Rigidbody rb;
        
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void ThisDestroy()
        {
            Debug.Log($"Destroying projectile {gameObject}");
            Destroy(gameObject);
        }
        
        private void OnValidate()
        {
            if (!gameObject.TryGetComponent(out Rigidbody _))
            {
                throw new Exception("This behaviour requires a RigidBody component");
            }
            
            if (!gameObject.TryGetComponent(out Collider _))
            {
                throw new Exception("This behaviour requires a Collider component");
            }
        }
    }
}
