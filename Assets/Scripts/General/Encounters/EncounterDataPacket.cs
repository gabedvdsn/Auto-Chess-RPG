using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoChessRPG
{
    public struct EncounterDataPacket
    {
        public List<Character> enemyCharacters;
        public Dictionary<Character, LootDropsPacket> lootDropsPackets;

        public EncounterDataPacket(List<Character> _enemyCharacters, Dictionary<Character, LootDropsPacket> _lootDropsPackets)
        {
            enemyCharacters = _enemyCharacters;
            lootDropsPackets = _lootDropsPackets;
        }
    }

    public struct LootDropsPacket
    {
        public int maxDrops;
        public Dictionary<BaseItemData, float> drops;

        public LootDropsPacket(int _maxDrops, Dictionary<BaseItemData, float> _drops)
        {
            maxDrops = _maxDrops;
            drops = _drops;

            maxDrops = Mathf.Clamp(maxDrops, 0, drops.Count);
        }

        public List<BaseItemData> GetDrops(float dropCoefficient)
        {
            int itemsToDrop = Mathf.CeilToInt(maxDrops * dropCoefficient);
            List<BaseItemData> droppedItems = new List<BaseItemData>();

            BaseItemData[] possibleDrops = drops.Keys.ToArray();

            while (droppedItems.Count < itemsToDrop)
            {
                BaseItemData possibleDrop = possibleDrops[Random.Range(0, drops.Count)];
                if (Random.Range(0, 1) < drops[possibleDrop]) droppedItems.Add(possibleDrop);
            }

            return droppedItems;
        }
    }
}
