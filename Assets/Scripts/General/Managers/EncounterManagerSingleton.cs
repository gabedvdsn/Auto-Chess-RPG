using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoChessRPG
{
    public class EncounterManagerSingleton : MonoBehaviour
    {
        public static EncounterManagerSingleton Instance;
        
        private Dictionary<Affiliation, List<CharacterEntityController>> controllersInEncounter;
        private EncounterRecordPacket record;

        public void Initialize(Dictionary<Affiliation, List<CharacterEntityController>> _controllersInEncounter)
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

        public Vector3 PerformMoveAction(CharacterEntityController self, CharacterControlAction nextAction, CharacterEntityController currTarget = null)
        {
            
        }
        
        #endregion

        #region ReTarget
        private const ReTargetMethod DEFAULT_FALLBACK_RETARGET_METHOD = ReTargetMethod.Closest;

        private void AddToReTargetRecord(ReTargetMethod targetMethod, CharacterEntityController self, CharacterEntityController currTarget, CharacterEntityController newTarget,
            float reTargetAmount)
        {
            record.reTargetRecord[Time.time] = new ReTargetRecordPacket(targetMethod, self, currTarget, newTarget, reTargetAmount);
        }
        
        public CharacterEntityController PerformReTargetAction(CharacterEntityController self, CharacterEntityController currTarget,
            ReTargetMethod targetMethod, ReTargetMethod secondaryTargetMethod = ReTargetMethod.Closest, bool secondarySearch = false)
        {
            var targetAffiliation = AffiliationManager.GetOpposingAffiliation(self.GetCharacter().GetAffiliation());
            var result = ReTarget(self, targetAffiliation, currTarget, targetMethod, secondaryTargetMethod);

            AddToReTargetRecord(targetMethod, self, currTarget, result.Item1, result.Item2);

            return result.Item1;
        }

        private (CharacterEntityController, float) ReTarget(CharacterEntityController self, Affiliation targetAffiliation, CharacterEntityController currTarget = null,
            ReTargetMethod targetMethod = ReTargetMethod.Closest, ReTargetMethod secondaryTargetMethod = ReTargetMethod.Closest, List<CharacterEntityController> toCheck = null)
        {
            return targetMethod switch
            {
                ReTargetMethod.Closest => ReTargetClosest(self, targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostHealth => ReTargetMostHealth(targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostMana => ReTargetMostMana(targetAffiliation, currTarget, toCheck),
                ReTargetMethod.MostDangerous => ReTargetMostDangerous(self, targetAffiliation, secondaryTargetMethod, currTarget, toCheck),
                _ => (currTarget, 0f)
            };
        }

        private (CharacterEntityController, float) ReTargetClosest(CharacterEntityController self, Affiliation targetAffiliation, CharacterEntityController currTarget = null,
            List<CharacterEntityController> toCheck = null)
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

        private (CharacterEntityController, float) ReTargetMostHealth(Affiliation targetAffiliation, CharacterEntityController currTarget = null,
            List<CharacterEntityController> toCheck = null)
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

        private (CharacterEntityController, float) ReTargetMostMana(Affiliation targetAffiliation, CharacterEntityController currTarget = null,
            List<CharacterEntityController> toCheck = null)
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

        private (CharacterEntityController, float) ReTargetMostDangerous(CharacterEntityController self, Affiliation targetAffiliation, ReTargetMethod secondaryTargetMethod,
            CharacterEntityController currTarget = null, List<CharacterEntityController> toCheck = null)
        {
            var possibleControllers = new List<CharacterEntityController>();

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
        public Dictionary<Affiliation, List<CharacterEntityController>> controllersInEncounter;
        public Dictionary<float, MoveRecordPacket> moveRecord;
        public Dictionary<float, ReTargetRecordPacket> reTargetRecord;

        public EncounterRecordPacket(Dictionary<Affiliation, List<CharacterEntityController>> _controllersInEncounter)
        {
            controllersInEncounter = _controllersInEncounter;

            moveRecord = new Dictionary<float, MoveRecordPacket>();
            reTargetRecord = new Dictionary<float, ReTargetRecordPacket>();
        }
    }

    public struct MoveRecordPacket
    {
        public CharacterEntityController self;
        public Vector3 initialPosition;
        public Vector3 targetPosition;

        public MoveRecordPacket(CharacterEntityController _self, Vector3 _initialPosition, Vector3 _targetPosition)
        {
            self = _self;
            initialPosition = _initialPosition;
            targetPosition = _targetPosition;
        }
    }

    public struct ReTargetRecordPacket
    {
        public ReTargetMethod reTargetMethod;
        public CharacterEntityController self;
        public CharacterEntityController initialTarget;
        public CharacterEntityController newTarget;
        public float reTargetAmount;

        public ReTargetRecordPacket(ReTargetMethod _reTargetMethod, CharacterEntityController _self, CharacterEntityController _initialTarget, CharacterEntityController _newTarget,
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
