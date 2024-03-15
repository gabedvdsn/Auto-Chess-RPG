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
        private Dictionary<int, UnspecifiedAbilityMeta> shelf;
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

        public bool MoveAbility(UnspecifiedAbilityMeta baseAbility, int slotToMoveTo)
        {
            if (shelf.Values.Contains(baseAbility)) return false;

            int currSlot = 0;

            foreach (int slot in shelf.Keys.Where(slot => shelf[slot] == baseAbility)) currSlot = slot;

            int newSlot = Mathf.Clamp(slotToMoveTo, 0, size - 1);

            if (shelf[newSlot] is not null)
            {
                UnspecifiedAbilityMeta displacedBaseAbility = shelf[newSlot];
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

        public bool AddAbility(UnspecifiedAbilityMeta baseAbility, int abilitySlot)
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

            return true;
        }

        public bool RemoveAbility(UnspecifiedAbilityMeta baseAbility)
        {
            if (!shelf.Values.Contains(baseAbility)) return false;

            int i = shelf.Keys.TakeWhile(abilitySlot => shelf[abilitySlot] != baseAbility).Count();

            shelf[i] = null;

            return true;
        }

        public bool RemoveAbility(int index)
        {
            if (!(0 <= index && index < shelf.Count)) return false;
            
            if (shelf[index] is null) return false;

            UnspecifiedAbilityMeta baseAbility = shelf[index];

            shelf[index] = null;

            return true;
        }

        public bool Initialize(int shelfSize, UnspecifiedAbilityMeta[] abilities)
        {
            if (shelf is not null) return false;

            shelf = new Dictionary<int, UnspecifiedAbilityMeta>();
            size = shelfSize;
            
            FillShelfWithNull();
            
            for (int i = 0; i < size; i++)
            {
                if (i >= size) return false;

                shelf[i] = abilities[i];
            }
            
            return true;
        }

        private void FillShelfWithNull()
        {
            for (int i = 0; i < size; i++) shelf[i] = null;
        }
        
        public bool OnUseAbility(UnspecifiedAbilityMeta baseAbility)
        {
            return true;
        }

        public bool AbilityIsOffCooldown(UnspecifiedAbilityMeta ability) => ability.IsOffCooldown();

        public bool AbilityIsOffCooldown(int index)
        {
            if (!(0 <= index && index < shelf.Count)) return false;
            
            return shelf[index].IsOffCooldown();
        }

        public UnspecifiedAbilityMeta[] GetShelf() => shelf.Values.ToArray();

        public RealAbilityData[] GetShelfRealAbilityDatas()
        {
            List<RealAbilityData> realAbilityDatas = new List<RealAbilityData>();
            
            foreach (UnspecifiedAbilityMeta meta in shelf.Values)
            {
                realAbilityDatas.Add(meta.GetRealData());
            }

            return realAbilityDatas.ToArray();
        }
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
