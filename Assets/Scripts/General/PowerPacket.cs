using System;

namespace AutoChessRPG
{
    public class RealPowerPacket
    {
        public int power;
        
        public int level;
        public BasePowerPacket basePacket;

        public RealPowerPacket(BasePowerPacket _basePacket, int _power = 0, int _level = 1)
        {
            basePacket = _basePacket;
            power = _power;
            level = _level;
        }

        public bool LevelUp()
        {
            // Only adds to level, does not update power
            if (level >= basePacket.maxLevel) return false;

            // If returns true, then level was added
            level += 1;

            return true;
        }
    }

    [Serializable]
    public class BasePowerPacket
    {
        public Rarity rarity;
        public int maxLevel;
    }
}
