using System;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

namespace AutoChessRPG
{
    public class TestProjectileCastAbility : RealProjectileCastAbility, IRealAbility
    {
        [SerializeField] private GameObject target;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ShootProjectile(target);
            }
        }

        public bool OnLevelUp()
        {
            if (!data.LevelUp()) return false;

            data.GetPowerPacket().power = PowerGenerator.GetAbilityPower(data);

            return true;
        }

        public RealPowerPacket GetPowerPacket() => data.GetPowerPacket();
    }
}
