using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Aura")]
    public class BaseAuraAbilityData : BaseAbilityData
    {
        public RealAuraAbilityData ToRealAuraAbility(int levels, RealItemData _attachedItem)
        {
            RealAuraAbilityData real = new RealAuraAbilityData(this, _attachedItem);

            real.OnLevelsUp(levels);

            return real;
        }
    }

    
}
