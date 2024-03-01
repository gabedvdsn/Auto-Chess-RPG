using System;

namespace AutoChessRPG
{
    public struct RealPowerPacket
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
            if (level >= basePacket.maxLevel) return false;

            level += 1;

            return true;
        }
    }

    [Serializable]
    public struct BasePowerPacket
    {
        public Rarity rarity;
        public int maxLevel;
    }
}
