using System.Collections.Generic;

namespace AutoChessRPG
{
    public static class EncounterParameters
    {
        private static Dictionary<ReTargetPreference, ReTargetMethod> DEFAULT_RETARGET_PREFERENCES = new Dictionary<ReTargetPreference, ReTargetMethod>()
        {
            { ReTargetPreference.Optimal , ReTargetMethod.NONE }
        };

        private static Dictionary<BaseAbilityData, CastUsagePreference> DEFAULT_ABILITY_USAGE_PREFERENCES = new Dictionary<BaseAbilityData, CastUsagePreference>()
        {
            
        };
        
        private static Dictionary<BaseItemData, CastUsagePreference> DEFAULT_ITEM_USAGE_PREFERENCES = new Dictionary<BaseItemData, CastUsagePreference>()
        {
            
        };

        public static Dictionary<ReTargetPreference, ReTargetMethod> GetDefaultReTargetPreferences() => DEFAULT_RETARGET_PREFERENCES;
        public static Dictionary<BaseAbilityData, CastUsagePreference> GetDefaultAbilityUsagePreferences() => DEFAULT_ABILITY_USAGE_PREFERENCES;
        public static Dictionary<BaseItemData, CastUsagePreference> GetDefaultItemUsagePreferences() => DEFAULT_ITEM_USAGE_PREFERENCES;
    }
}
