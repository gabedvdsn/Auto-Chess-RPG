using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BLifeTime : GameObjectBehaviour
    {
        [SerializeField] private float lifetime;

        protected new void Initialize()
        {
            base.Initialize();
        }

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
