using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class ItemShelf : ObservableSubject
    {
        private Dictionary<ItemData, float> shelf;
        private List<ItemRecord> records;

        public bool PassItemsOnInstantiation(ItemData[] items)
        {
            if (shelf is not null) return false;

            shelf = new Dictionary<ItemData, float>();

            foreach (ItemData item in items) shelf[item] = 0f;

            return true;
        }

        public bool OnUseItem(ItemData item)
        {
            if (item.GetAttachedAbilities().Length > 0)
            {
                
            }

            return true;
        }

        private bool ItemIsOffCooldown(ItemData ability) => shelf[ability] <= 0f;
    }

    public struct ItemRecord : IObservableData
    {
        public ItemData baseData;

        public float timeInitial;
        public float timeFinal;

        public Dictionary<Character, EffectRecord> itemImpact;

        public ItemRecord(ItemData _baseData, float _timeInitial)
        {
            baseData = _baseData;

            timeInitial = _timeInitial;
            timeFinal = 0f;

            itemImpact = new Dictionary<Character, EffectRecord>();
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
