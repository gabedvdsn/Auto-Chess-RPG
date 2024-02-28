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
            // loot depends on many things, mainly being:
            //  power
            //  affinity
            //  attribute
        }

        public static float GetAveragePowerFromList(List<Character> characters)
        {
            float power = 0f;
            foreach (Character character in characters)
            {
                
            }
        }
    }
}
