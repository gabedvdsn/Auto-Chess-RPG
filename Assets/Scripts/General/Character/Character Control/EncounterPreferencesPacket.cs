using System;
using System.Collections.Generic;

namespace AutoChessRPG
{
    [Serializable]
    public class EncounterPreferencesPacket
    {
        public Dictionary<ReTargetPreference, ReTargetMethod> retargetPreferences;
        public Dictionary<AbilityData, CastUsagePreference> abilityUsagePreferences;
        public Dictionary<AbilityData, CastUsagePreference> itemUsagePreferences;
    }
}
