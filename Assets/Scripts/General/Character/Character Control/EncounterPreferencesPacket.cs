using System;
using System.Collections.Generic;

namespace AutoChessRPG
{
    [Serializable]
    public class EncounterPreferencesPacket
    {
        public Dictionary<ReTargetPreference, ReTargetMethod> retargetPreferences = EncounterParameters.GetDefaultReTargetPreferences();
        public Dictionary<AbilityData, CastUsagePreference> abilityUsagePreferences = EncounterParameters.GetDefaultAbilityUsagePreferences();
        public Dictionary<ItemData, CastUsagePreference> itemUsagePreferences = EncounterParameters.GetDefaultItemUsagePreferences();

    }
}
