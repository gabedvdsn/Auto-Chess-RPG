using System.Collections.Generic;

namespace AutoChessRPG
{
    public static class LootDropGenerator
    {
        public static Dictionary<Character, LootDropsPacket> GetPowerSpecificLootDropsPackets(List<Character> characters, List<Character> alliesReference = null)
        {
            Dictionary<Character, LootDropsPacket> lootDropsPackets = new Dictionary<Character, LootDropsPacket>();

            foreach (Character character in characters) lootDropsPackets[character] = GetPowerSpecificLootDropsPacket(character);

            return lootDropsPackets;
        }

        public static LootDropsPacket GetPowerSpecificLootDropsPacket(Character character)
        {
            /*
             * Loot drops depend on two things:
             *  power (level & rarity of loot drops) => a character with high power will drop items with greater level and rarity
             *  affinity (affinity of loot drops, loosely) => a character with a certain affinity will drop items with that affinity, or no affinity
             *  attributes (type of loot drops, loosely) => a character with high relative int will drop items with high relative int
             */
            
        }
    }
}
