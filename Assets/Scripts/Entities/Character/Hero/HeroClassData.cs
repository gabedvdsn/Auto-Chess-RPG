using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AutoChessRPG.Entity.Character
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Class")]
    public class HeroClassData : ScriptableObject
    {
        [Header("Class Information")] 
        [SerializeField] private string className;
        [FormerlySerializedAs("attributes")] [SerializeField] private BaseAttributePacket baseAttributes;
    }
}
