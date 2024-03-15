using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public enum CharacterModifierTag
    {
        // Direct stats
        ModifyCurrentHealth,
        ModifyCurrentMana,
        ModifyMovespeed,
        ModifyAttackSpeed,
        ModifyArmor,
        ModifyNegation,
        ModifyMagicalResistance,
        ModifyPhysicalResistance,
        ModifyDebuffResistance,
        ModifyAttackDamage,
        ModifyMaxHealth,
        ModifyMaxMana,
        ModifyHealthRegen,
        ModifyManaRegen,
        ModifyEvasion,
        
        // Other
        DoForcedMovement
        
    }

}