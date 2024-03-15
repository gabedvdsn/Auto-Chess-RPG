using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

namespace AutoChessRPG
{
    public class ACTestProjectileFollowAndCollideCastAbility : RealProjectileFollowAndCollideCastAbilityMeta, ICreatesRealProjectile
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject hitEffect;

        protected override void Awake()
        {
            onObjectCollisionAction = OnProjectileHit;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Execute(new TargetingPacket(_targetGO: target, _targetPos: null));
            }
        }

        public void OnProjectileHit(EncounterAutoCharacterController controller)
        {
            Debug.Log($"Doing stuff to {controller.GetAffiliation()} {controller.GetCharacterData().GetEntityName()}");
            
            // Instantiate(hitEffect, controller.transform.position, Quaternion.identity);
        }

        public bool OnLevelUp()
        {
            if (!data.OnLevelUp()) return false;

            data.GetPowerPacket().power = PowerGenerator.GetAbilityPower(data);

            return true;
        }

        public RealPowerPacket GetPowerPacket() => data.GetPowerPacket();
    }
}
