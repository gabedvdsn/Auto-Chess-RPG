using UnityEngine;

namespace AutoChessRPG.Item_Types
{    
    [CreateAssetMenu(menuName = "Scriptable Objects/Game Entity/Item/Ability Item Base", fileName = "AbilityItem")]
    public class AbilityItemData : AttributeItemData
    {
        [Header("Ability Data Information")] 
        [SerializeField] private AbilityData[] attachedAbilities;
    }
}