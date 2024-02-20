using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG.Entity.Character
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Class")]
    public class HeroClassData : ScriptableObject
    {
        [Header("Class Information")] 
        [SerializeField] private string className;
        [SerializeField] private AttributePacket attributes;
    }
}
