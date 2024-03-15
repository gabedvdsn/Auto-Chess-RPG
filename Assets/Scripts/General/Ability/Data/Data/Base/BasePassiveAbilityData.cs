using UnityEngine;

namespace AutoChessRPG
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Ability/Passive")]
    public class BasePassiveAbilityData : BaseAbilityData
    {
        public RealPassiveAbilityData ToRealPassiveAbility(int level = 0, RealItemData _attachedItem = null)
        {
            RealPassiveAbilityData real = new RealPassiveAbilityData(this, _attachedItem);

            real.OnLevelsUp(level);

            return real;
        }
    }

    
}
