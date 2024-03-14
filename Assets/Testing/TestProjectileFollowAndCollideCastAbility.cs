using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

namespace AutoChessRPG
{
    public class TestProjectileFollowAndCollideCastAbility : RealProjectileFollowAndCollideCastAbility, IRealProjectile
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject hitEffect;

        private void Awake()
        {
            onProjectileHitAction = OnProjectileHit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ShootProjectile(target);
            }
        }

        public void OnProjectileHit(EncounterAutoCharacterController controller)
        {
            Debug.Log($"Doing stuff to {controller.GetAffiliation()} {controller.GetCharacterData().GetEntityName()}");
            
            // Instantiate(hitEffect, controller.transform.position, Quaternion.identity);
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
