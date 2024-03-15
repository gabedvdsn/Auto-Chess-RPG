using System.Collections.Generic;
using System.Linq;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class ItemShelf : ObservableSubject
    {
        private Dictionary<RealItemData, float> shelf;
        private List<ItemRecord> records;

        public bool PassItemsOnInstantiation(RealItemData[] items)
        {
            if (shelf is not null) return false;

            shelf = new Dictionary<RealItemData, float>();

            foreach (RealItemData item in items) shelf[item] = 0f;

            return true;
        }

        public bool OnUseItem(RealItemData item)
        {
            if (!ItemIsOffCooldown(item)) return false;
            
            shelf[item] = item.

            return true;
        }

        public bool ItemIsOffCooldown(RealItemData item) => shelf[item] <= 0f && item.GetCooldown() >= 0;

        public RealItemData[] GetShelf() => shelf.Keys.ToArray();
    }

    public struct ItemRecord : IObservableData
    {
        public BaseItemData baseData;

        public float timeInitial;
        public float timeFinal;

        public Dictionary<Character, EffectRecord> itemImpact;

        public ItemRecord(BaseItemData _baseData, float _timeInitial)
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
