using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class BLifeTime : GameObjectBehaviour
    {
        [SerializeField] private float lifetime;

        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
