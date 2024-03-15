using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoChessRPG
{
    [Serializable]
    public class EncounterPreferencesPacket
    {
        public Dictionary<ReTargetPreference, ReTargetMethod> retargetPreferences = EncounterParameters.GetDefaultReTargetPreferences();
        public Dictionary<RealAbilityData, CastUsagePreference> abilityUsagePreferences = EncounterParameters.GetDefaultAbilityUsagePreferences();
        public Dictionary<RealItemData, CastUsagePreference> itemUsagePreferences = EncounterParameters.GetDefaultItemUsagePreferences();

        public Dictionary<RealAbilityData, CastUsagePreference> combinedAbilityUsagePreferences;

        public EncounterPreferencesPacket()
        {
            combinedAbilityUsagePreferences = CombineAbilities();
        }

        public ReTargetMethod GetRetargetPreference(ReTargetPreference pref) => retargetPreferences[pref];

        public CastUsagePreference GetAbilityUsagePreference(RealAbilityData ability) => combinedAbilityUsagePreferences[ability];

        public CastUsagePreference GetItemUsagePreference(RealItemData item) => itemUsagePreferences[item];
        
        private Dictionary<RealAbilityData, CastUsagePreference> CombineAbilities()
        {
            Dictionary<RealAbilityData, CastUsagePreference> abilityPrefs = new Dictionary<RealAbilityData, CastUsagePreference>();

            foreach (RealAbilityData ability in abilityUsagePreferences.Keys)
            {
                abilityPrefs[ability] = abilityUsagePreferences[ability];
            }
            
            foreach (RealItemData item in itemUsagePreferences.Keys)
            {
                foreach (RealAbilityData ability in item.GetAttachedAbilities())
                {
                    ability.SendAttachedItem(item);
                    abilityPrefs[ability] = itemUsagePreferences[item];
                }
            }

            return abilityPrefs;
        }

    }
}
