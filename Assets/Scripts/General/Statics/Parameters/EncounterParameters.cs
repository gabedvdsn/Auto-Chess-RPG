using System.Collections.Generic;

namespace AutoChessRPG
{
    public static class EncounterParameters
    {
        private static Dictionary<ReTargetPreference, ReTargetMethod> DEFAULT_RETARGET_PREFERENCES = new Dictionary<ReTargetPreference, ReTargetMethod>()
        {
            { ReTargetPreference.Optimal , ReTargetMethod.NONE }
        };

        private static Dictionary<AbilityData, CastUsagePreference> DEFAULT_ABILITY_USAGE_PREFERENCES = new Dictionary<AbilityData, CastUsagePreference>()
        {
            
        };
        
        private static Dictionary<ItemData, CastUsagePreference> DEFAULT_ITEM_USAGE_PREFERENCES = new Dictionary<ItemData, CastUsagePreference>()
        {
            
        };

        public static Dictionary<ReTargetPreference, ReTargetMethod> GetDefaultReTargetPreferences() => DEFAULT_RETARGET_PREFERENCES;
        public static Dictionary<AbilityData, CastUsagePreference> GetDefaultAbilityUsagePreferences() => DEFAULT_ABILITY_USAGE_PREFERENCES;
        public static Dictionary<ItemData, CastUsagePreference> GetDefaultItemUsagePreferences() => DEFAULT_ITEM_USAGE_PREFERENCES;
    }
}
