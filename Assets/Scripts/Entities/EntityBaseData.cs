using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once CheckNamespace
namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity Base")]
    public class EntityBaseData : ScriptableObject
    {
        [Header("Entity Base Information")] 
        [SerializeField] private string entityLookupName;
        [Space] 
        [SerializeField] private string entityName;
        [SerializeField] private string entityDescription;
        [SerializeField] private string entityID;

        public string GetEntityLookupName() => entityLookupName;

        public string GetEntityName() => entityName;

        public string GetEntityDescription() => entityDescription;
        
        public string GetEntityID() => entityID;
    }
}
