using System;
using System.Collections.Generic;

namespace AutoChessRPG
{
    [Serializable]
    public class EncounterPreferencesPacket
    {
        public Dictionary<ReTargetPreference, ReTargetMethod> retargetPreferences = EncounterParameters.GetDefaultReTargetPreferences();
        public Dictionary<BaseAbilityData, CastUsagePreference> abilityUsagePreferences = EncounterParameters.GetDefaultAbilityUsagePreferences();
        public Dictionary<BaseItemData, CastUsagePreference> itemUsagePreferences = EncounterParameters.GetDefaultItemUsagePreferences();

    }
}
