using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace AutoChessRPG
{
    // AbilityShelf is a Mono component attached to character units
    // AbilityShelf contains all character's abilities
    // AbilityShelf is responsible for managing cooldowns and ability deployment
    public class AbilityShelf : ObservableSubject
    {
        private Dictionary<AbilityData, float> shelf;
        private List<AbilityRecord> records;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool PassAbilitiesOnInstantiation(AbilityData[] abilities)
        {
            if (shelf is not null) return false;

            shelf = new Dictionary<AbilityData, float>();

            foreach (AbilityData ability in abilities) shelf[ability] = 0f;
            
            return true;
        }

        public bool OnUseAbility(AbilityData ability)
        {
            return true;
        }

        private bool AbilityIsOffCooldown(AbilityData ability) => shelf[ability] <= 0f;
    }

    public struct AbilityRecord : IObservableData
    {
        public AbilityData baseData;

        public float timeInitial;
        public float timeFinal;

        public Dictionary<Character, EffectRecord> abilityImpact;

        public AbilityRecord(AbilityData _baseData, float _timeInitial)
        {
            baseData = _baseData;
            
            timeInitial = _timeInitial;
            timeFinal = 0f;

            abilityImpact = new Dictionary<Character, EffectRecord>();
        }
        
        public IObservableData OnObserve()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> FormatForObservance()
        {
            throw new System.NotImplementedException();
        }
    }
}
