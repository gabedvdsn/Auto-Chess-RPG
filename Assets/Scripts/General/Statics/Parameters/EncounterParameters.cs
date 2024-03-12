using System.Collections.Generic;

namespace AutoChessRPG
{
    public static class EncounterParameters
    {
        private static Dictionary<ReTargetPreference, ReTargetMethod> DEFAULT_RETARGET_PREFERENCES = new Dictionary<ReTargetPreference, ReTargetMethod>()
        {
            { ReTargetPreference.Optimal , ReTargetMethod.NONE }
        };

        private static Dictionary<RealAbilityData, CastUsagePreference> DEFAULT_ABILITY_USAGE_PREFERENCES = new Dictionary<RealAbilityData, CastUsagePreference>()
        {
            
        };
        
        private static Dictionary<RealItemData, CastUsagePreference> DEFAULT_ITEM_USAGE_PREFERENCES = new Dictionary<RealItemData, CastUsagePreference>()
        {
            
        };

        public static Dictionary<ReTargetPreference, ReTargetMethod> GetDefaultReTargetPreferences() => DEFAULT_RETARGET_PREFERENCES;
        public static Dictionary<RealAbilityData, CastUsagePreference> GetDefaultAbilityUsagePreferences() => DEFAULT_ABILITY_USAGE_PREFERENCES;
        public static Dictionary<RealItemData, CastUsagePreference> GetDefaultItemUsagePreferences() => DEFAULT_ITEM_USAGE_PREFERENCES;
    }
}
