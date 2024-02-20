using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Attributes", fileName = "ItemAttributes")]

    public class ItemAttributesData : ScriptableObject
    {
        [SerializedDictionary("Attribute", "Value")] 
        [SerializeField] private SerializedDictionary<Attribute, float> attributes;

        public SerializedDictionary<Attribute, float> GetAttributes() => attributes;
    }
}
