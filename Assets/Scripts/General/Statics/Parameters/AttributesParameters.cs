using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace AutoChessRPG
{
    public static class AttributesParameters
    {
        public const float STR_TO_MAX_HEALTH = 23;  
        
        public const float INT_TO_MAX_MANAPOOL = 21;  

        public const float STR_TO_ATTACKDAMAGE = 1.5f;  
        public const float AGI_TO_ATTACKDAMAGE = 1.75f;  
        
        public const float AGI_TO_ATTACKSPEED = .06f; 
        
        public const float STR_TO_MOVESPEED = -.01f;  
        public const float AGI_TO_MOVESPEED = .04f;  
        
        public const float STR_TO_ARMOR = .15f;  
        public const float AGI_TO_ARMOR = .4f;  

        public const float ARMOR_TO_PHYSRESIST = .8f / 100f; 
            
        public const float INT_TO_NEGATION = .35f;  

        public const float NEGATION_TO_MAGRESIST = 1.25f / 100f; 
        
        public const float STR_TO_HEALTHREGEN = .4f; 
        
        public const float INT_TO_MANAREGEN = .55f;  

        public const float PHYSRESIST_TO_DEBUFFRESIST = .5f; 
        public const float MAGRESIST_TO_DEBUGGRESIST = .5f;  
    }
}
