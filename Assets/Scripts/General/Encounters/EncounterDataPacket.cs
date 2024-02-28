using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoChessRPG
{
    public struct EncounterDataPacket
    {
        public List<Character> enemyCharacters;
        public Dictionary<Character, LootDropsPacket> lootDropsPackets;

        public EncounterDataPacket(List<Character> _enemyCharacters)
        {
            enemyCharacters = _enemyCharacters;
            
        }
    }

    public struct LootDropsPacket
    {
        public int maxDrops;
        public Dictionary<ItemData, float> drops;

        public LootDropsPacket(int _maxDrops, Dictionary<ItemData, float> _drops)
        {
            maxDrops = _maxDrops;
            drops = _drops;

            maxDrops = Mathf.Clamp(maxDrops, 0, drops.Count);
        }

        public List<ItemData> GetDrops(float dropCoefficient)
        {
            int itemsToDrop = Mathf.CeilToInt(maxDrops * dropCoefficient);
            List<ItemData> droppedItems = new List<ItemData>();

            ItemData[] possibleDrops = drops.Keys.ToArray();

            while (droppedItems.Count < itemsToDrop)
            {
                ItemData possibleDrop = possibleDrops[Random.Range(0, drops.Count)];
                if (Random.Range(0, 1) < drops[possibleDrop]) droppedItems.Add(possibleDrop);
            }

            return droppedItems;
        }
    }
}
