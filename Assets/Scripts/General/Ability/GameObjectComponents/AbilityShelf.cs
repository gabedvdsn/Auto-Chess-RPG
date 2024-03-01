using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace AutoChessRPG
{
    // AbilityShelf is a Mono component attached to character units
    // AbilityShelf contains all character's abilities
    // AbilityShelf is responsible for managing cooldowns and baseAbility deployment
    public class AbilityShelf : ObservableSubject
    {
        private Dictionary<int, BaseAbilityData> shelf;
        private Dictionary<BaseAbilityData, float> cooldowns;
        private List<AbilityRecord> records;

        private int size;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool MoveAbility(BaseAbilityData baseAbility, int slotToMoveTo)
        {
            if (shelf.Values.Contains(baseAbility)) return false;

            int currSlot = 0;

            foreach (int slot in shelf.Keys.Where(slot => shelf[slot] == baseAbility)) currSlot = slot;

            int newSlot = Mathf.Clamp(slotToMoveTo, 0, size - 1);

            if (shelf[newSlot] is not null)
            {
                BaseAbilityData displacedBaseAbility = shelf[newSlot];
                shelf[newSlot] = baseAbility;
                shelf[currSlot] = displacedBaseAbility;
            }
            else
            {
                shelf[newSlot] = baseAbility;
                shelf[currSlot] = null;
            }

            return true;
            
            
        }

        public bool AddAbility(BaseAbilityData baseAbility, int abilitySlot)
        {
            if (shelf.Count + 1 > size) return false;
            if (shelf.Values.Contains(baseAbility)) return false;

            int slot = Mathf.Clamp(abilitySlot, 0, size - 1);

            if (shelf.Keys.Contains(slot))
            {
                for (int i = shelf.Count - 1; i >= slot; --i)
                {
                    shelf[i + 1] = shelf[i];
                }
            }

            shelf[slot] = baseAbility;
            cooldowns[baseAbility] = 0f;

            return true;
        }

        public bool RemoveAbility(BaseAbilityData baseAbility)
        {
            if (!shelf.Values.Contains(baseAbility)) return false;

            int i = shelf.Keys.TakeWhile(abilitySlot => shelf[abilitySlot] != baseAbility).Count();

            shelf[i] = null;
            cooldowns.Remove(baseAbility);

            return true;
        }

        public bool RemoveAbility(int abilitySlot)
        {
            if (shelf[abilitySlot] is null) return false;

            BaseAbilityData baseAbility = shelf[abilitySlot];

            shelf[abilitySlot] = null;
            cooldowns.Remove(baseAbility);

            return true;
        }

        public bool Initialize(int shelfSize, BaseAbilityData[] abilities)
        {
            if (cooldowns is not null) return false;

            size = shelfSize;
            
            FillShelfWithNull();

            cooldowns = new Dictionary<BaseAbilityData, float>();

            for (int i = 0; i < size; i++)
            {
                if (i >= size) return false;

                shelf[i] = abilities[i];
                cooldowns[abilities[i]] = 0f;
            }
            
            return true;
        }

        private void FillShelfWithNull()
        {
            for (int i = 0; i < size; i++) shelf[i] = null;
        }

        

        public bool OnUseAbility(BaseAbilityData baseAbility)
        {
            return true;
        }

        private bool AbilityIsOffCooldown(BaseAbilityData baseAbility) => cooldowns[baseAbility] <= 0f;
    }

    public struct AbilityRecord : IObservableData
    {
        public BaseAbilityData baseData;

        public float timeInitial;
        public float timeFinal;

        public Dictionary<Character, EffectRecord> abilityImpact;

        public AbilityRecord(BaseAbilityData _baseData, float _timeInitial)
        {
            baseData = _baseData;
            
            timeInitial = _timeInitial;
            timeFinal = 0f;

            abilityImpact = new Dictionary<Character, EffectRecord>();
        }
        
        public IObservableData OnObserve()
        {
            return this;
        }

        public Dictionary<string, string> FormatForObservance()
        {
            throw new System.NotImplementedException();
        }
    }
}
