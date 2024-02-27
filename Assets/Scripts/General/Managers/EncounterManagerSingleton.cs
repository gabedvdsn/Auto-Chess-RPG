using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class EncounterManagerSingleton : MonoBehaviour
    {
        public static EncounterManagerSingleton Instance;
        
        private Dictionary<Affiliation, List<EncounterAutoCharacterController>> controllersInEncounter;
        private EncounterRecordPacket record;

        public void Initialize(Dictionary<Affiliation, List<EncounterAutoCharacterController>> _controllersInEncounter)
        {
            controllersInEncounter = _controllersInEncounter;
            record = new EncounterRecordPacket(controllersInEncounter);
        }

        public EncounterRecordPacket GetRecordPacket() => record;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        #region Move

        /*
         * Move
         * Given some offset, find a position towards some currTarget until
         * Requires currTarget: True (can be null)
         * Requires nextAction: False
         * Requires offset: True
         */
        
        public Vector3 PerformMoveAction(EncounterAutoCharacterController self, CharacterControlAction nextAction, float offset, EncounterAutoCharacterController currTarget = null)
        {
            return Vector3.zero;
        }
        
        #endregion

        #region ReTarget
        
        /*
         * ReTarget
         * Find some new target character that is not the current target, unless the current target is the only possible target
         * Requires currTarget: True (can be null)
         * Requires nextAction: False
         */
        
        private const ReTargetMethod DEFAULT_FALLBACK_RETARGET_METHOD = ReTargetMethod.Closest;

        private void AddToReTargetRecord(ReTargetMethod targetMethod, EncounterAutoCharacterController self, EncounterAutoCharacterController currTarget, EncounterAutoCharacterController newTarget,
            float reTargetAmount)
        {
            record.reTargetRecord[Time.time] = new ReTargetRecordPacket(targetMethod, self, currTarget, newTarget, reTargetAmount);
        }
        
        public EncounterAutoCharacterController PerformReTargetAction(EncounterAutoCharacterController self, EncounterAutoCharacterController currTarget,
            ReTargetMethod targetMethod, ReTargetMethod secondaryTargetMethod = ReTargetMethod.Closest, bool secondarySearch = false)
        {
            var targetAffiliation = AffiliationManager.GetOpposingAffiliation(self.GetCharacter().GetAffiliation());
            var result = ReTarget(self, targetAffiliation, currTarget, targetMethod, secondaryTargetMethod);

            AddToReTargetRecord(targetMethod, self, currTarget, result.Item1, result.Item2);

            return result.Item1;
        }

        private (EncounterAutoCharacterController, float) ReTarget(EncounterAutoCharacterController self, Affiliation targetAffiliation, EncounterAutoCharacterController currTarget = null,
            ReTargetMethod targetMethod = ReTargetMethod.Closest, ReTargetMethod secondaryTargetMethod = ReTargetMethod.Closest, List<EncounterAutoCharacterController> toCheck = null)
        {
            return targetMethod switch
            {
                ReTargetMethod.Closest => ReTargetClosest(self, targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostHealth => ReTargetMostHealth(targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostMana => ReTargetMostMana(targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostAttack => ReTargetMostAttack(targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostDangerous => ReTargetMostDangerous(self, targetAffiliation, secondaryTargetMethod, currTarget, toCheck),
                _ => (currTarget, 0f)
            };
        }

        private (EncounterAutoCharacterController, float) ReTargetClosest(EncounterAutoCharacterController self, Affiliation targetAffiliation, EncounterAutoCharacterController currTarget = null,
            List<EncounterAutoCharacterController> toCheck = null)
        {
            var closest = currTarget;
            float closestDistance = currTarget is null ? 0 : Vector3.Distance(self.transform.position, closest.transform.position);

            foreach (var controller in toCheck ?? controllersInEncounter[targetAffiliation])
            {
                if (controller == closest) continue;

                float distance = Vector3.Distance(self.transform.position, controller.transform.position);

                if (!(distance < closestDistance)) continue;

                closest = controller;
                closestDistance = distance;
            }

            return (closest, closestDistance);
        }

        private (EncounterAutoCharacterController, float) ReTargetMostHealth(Affiliation targetAffiliation, EncounterAutoCharacterController currTarget = null,
            List<EncounterAutoCharacterController> toCheck = null)
        {
            var healthiest = currTarget;
            float healthiestAmount = currTarget is null ? 0 : currTarget.GetCharacterStatPacket().currHealth;

            foreach (var controller in toCheck ?? controllersInEncounter[targetAffiliation])
            {
                if (controller == healthiest) continue;

                float health = controller.GetCharacterStatPacket().currHealth;

                if (!(health < healthiestAmount)) continue;

                healthiest = controller;
                healthiestAmount = health;
            }

            return (healthiest, healthiestAmount);
        }

        private (EncounterAutoCharacterController, float) ReTargetMostMana(Affiliation targetAffiliation, EncounterAutoCharacterController currTarget = null,
            List<EncounterAutoCharacterController> toCheck = null)
        {
            var manaest = currTarget;
            float manaestAmount = currTarget is null ? 0 : currTarget.GetCharacterStatPacket().currMana;

            foreach (var controller in toCheck ?? controllersInEncounter[targetAffiliation])
            {
                if (controller == manaest) continue;

                float health = controller.GetCharacterStatPacket().currMana;

                if (!(health < manaestAmount)) continue;

                manaest = controller;
                manaestAmount = health;
            }

            return (manaest, manaestAmount);
        }
        
        private (EncounterAutoCharacterController, float) ReTargetMostAttack(Affiliation targetAffiliation, EncounterAutoCharacterController currTarget = null,
            List<EncounterAutoCharacterController> toCheck = null)
        {
            var attackest = currTarget;
            float attackAmounht = currTarget is null ? 0 : currTarget.GetCharacterStatPacket().attackDamage;

            foreach (var controller in toCheck ?? controllersInEncounter[targetAffiliation])
            {
                if (controller == attackest) continue;

                float health = controller.GetCharacterStatPacket().attackDamage;

                if (!(health < attackAmounht)) continue;

                attackest = controller;
                attackAmounht = health;
            }

            return (attackest, attackAmounht);
        }

        private (EncounterAutoCharacterController, float) ReTargetMostDangerous(EncounterAutoCharacterController self, Affiliation targetAffiliation, ReTargetMethod secondaryTargetMethod,
            EncounterAutoCharacterController currTarget = null, List<EncounterAutoCharacterController> toCheck = null)
        {
            var possibleControllers = new List<EncounterAutoCharacterController>();

            foreach (var controller in toCheck ?? controllersInEncounter[targetAffiliation])
            {
                if (controller.GetTarget() == self) possibleControllers.Add(controller);
            }
            
            if (possibleControllers.Count == 0)
            {
                // No enemies are targeting self, re-filter under secondaryMethod
                return ReTarget(self, targetAffiliation, currTarget, secondaryTargetMethod, DEFAULT_FALLBACK_RETARGET_METHOD);
            }

            // Filter under secondaryMethod using the list of targeting characters
            return ReTarget(self, targetAffiliation, currTarget, secondaryTargetMethod, DEFAULT_FALLBACK_RETARGET_METHOD, possibleControllers);
        }
        #endregion
    }

    public class EncounterRecordPacket
    {
        public Dictionary<Affiliation, List<EncounterAutoCharacterController>> controllersInEncounter;
        public Dictionary<float, MoveRecordPacket> moveRecord;
        public Dictionary<float, ReTargetRecordPacket> reTargetRecord;

        public EncounterRecordPacket(Dictionary<Affiliation, List<EncounterAutoCharacterController>> _controllersInEncounter)
        {
            controllersInEncounter = _controllersInEncounter;

            moveRecord = new Dictionary<float, MoveRecordPacket>();
            reTargetRecord = new Dictionary<float, ReTargetRecordPacket>();
        }
    }

    public struct MoveRecordPacket
    {
        public EncounterAutoCharacterController self;
        public Vector3 initialPosition;
        public Vector3 targetPosition;

        public MoveRecordPacket(EncounterAutoCharacterController _self, Vector3 _initialPosition, Vector3 _targetPosition)
        {
            self = _self;
            initialPosition = _initialPosition;
            targetPosition = _targetPosition;
        }
    }

    public struct ReTargetRecordPacket
    {
        public ReTargetMethod reTargetMethod;
        public EncounterAutoCharacterController self;
        public EncounterAutoCharacterController initialTarget;
        public EncounterAutoCharacterController newTarget;
        public float reTargetAmount;

        public ReTargetRecordPacket(ReTargetMethod _reTargetMethod, EncounterAutoCharacterController _self, EncounterAutoCharacterController _initialTarget, EncounterAutoCharacterController _newTarget,
            float _reTargetAmount)
        {
            reTargetMethod = _reTargetMethod;
            self = _self;
            initialTarget = _initialTarget;
            newTarget = _newTarget;
            reTargetAmount = _reTargetAmount;
        }
    }
}
