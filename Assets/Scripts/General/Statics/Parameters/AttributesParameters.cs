using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace AutoChessRPG
{
    public static class AttributesParameters
    {
        // Examples given are for character with 20 of each stat
        
        public const float STR_TO_MAX_HEALTH = 25f;  // 500 health
        
        public const float INT_TO_MAX_MANAPOOL = 22f;  // 440 mana

        public const float STR_TO_ATTACKDAMAGE = .75f;  // 15
        public const float AGI_TO_ATTACKDAMAGE = 2.25f;  // 45 -> 60 total damage per attack
        
        public const float AGI_TO_ATTACKSPEED = .06f;  // 1.2 attacks per second
        
        public const float STR_TO_MOVESPEED = -.03f;  // -.6
        public const float AGI_TO_MOVESPEED = .09f;  // 1.8 -> 1.2 total units per second
        
        public const float STR_TO_ARMOR = .15f;  // 3
        public const float AGI_TO_ARMOR = .4f;  // 8 -> 11 total armor

        public const float ARMOR_TO_PHYSRESIST = .8f / 100f;  // 8.8% or .088 phys resist
            
        public const float INT_TO_NEGATION = .35f;  // 7 negation (magic armor)

        public const float NEGATION_TO_MAGRESIST = 1.25f / 100f;  // 8.75% or .0875 mag resist
        
        public const float STR_TO_HEALTHREGEN = .4f;  // 8 health regen
        
        public const float INT_TO_MANAREGEN = .55f;  // 11 mana regen

        public const float PHYSRESIST_TO_DEBUFFRESIST = .5f;  // .044
        public const float MAGRESIST_TO_DEBUGGRESIST = .5f;  // .04375 -> 8.775% or .08775 total debuff resist
    }
}
