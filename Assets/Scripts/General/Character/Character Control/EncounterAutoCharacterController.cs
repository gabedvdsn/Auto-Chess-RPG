using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace AutoChessRPG
{
    public class EncounterAutoCharacterController : ObservableSubject
    {
        // Encounter information
        private Affiliation affiliation;
        private EncounterAutoCharacterController target;
        
        // Individual information
        private Character character;

        private AbilityShelf abilityShelf;
        private ItemShelf itemShelf;
        
        private CharacterMovement movement;

        private CharacterEntityData data;
        private StatPacket stats;
        
        // Status information
        private bool isDead
        {
            set => canAct = !value;
            get => isDead;
        }

        private bool canAttack;
        private bool canCast;
        private bool canMove = true;

        private bool canAct
        {
            set
            {
                canAttack = value;
                canMove = value;
                canCast = value;
                canAct = value;
            }
            get
            {
                return canAct; 
            }
        }
        
        // Control information
        private delegate bool QueuedActionDelegate();
        private QueuedActionDelegate queuedAction;

        private float queuedActionRange;
        private float distanceToTarget;

        private bool nothingToDo = true;
        private bool actionOverflow;
        private bool targetOverflow;
        
        private bool isTargeted;
        private EncounterAutoCharacterController targeter;

        private RealAbilityData queuedAbility;
        private RealItemData queuedItem;
        
        // Preferences information
        private ReTargetMethod alwaysReTargetMethod = ReTargetMethod.NONE;

        private void Update()
        {
            MainBranch();
        }

        public void Initialize(Affiliation _affiliation)
        {
            character = GetComponent<Character>();
            data = character.GetCharacterData();

            character.Initialize();
            
            stats = character.GetCharacterStatPacket();
            
            movement = GetComponent<CharacterMovement>();
            movement.Initialize(stats, 
                character.GetCharacterData().GetAllowableActionRange());
            
            affiliation = _affiliation;

            abilityShelf = character.GetCharacterAbilityShelf();
            itemShelf = character.GetCharacterItemShelf();
            
            nothingToDo = false;
        }
        
        #region Open

        public bool AttachEffect(ICharacterEntity _character, RealEffectData effect) => character.AttachEffect(_character, effect);

        public bool RemoveEffect(RealEffectData effect) => character.RemoveEffect(effect);
        
        #endregion
        
        #region Statuses

        public void SetPrimaryStatus(bool status) => canAct = status;

        public void SetDead(bool status = true) => isDead = status;
        
        private bool CharacterMayTarget() => movement.GetDegreesToTarget() < data.GetAllowableActionRange();
        
        #endregion
        
        #region Getters

        public Affiliation GetAffiliation() => affiliation;
        
        public Character GetCharacter() => character;

        public StatPacket GetCharacterStatPacket() => character.GetCharacterStatPacket();

        public CharacterMovement GetCharacterMovement() => movement;
        
        public EncounterAutoCharacterController GetTarget() => target;

        public CharacterEntityData GetCharacterData() => data;

        public bool GetPrimaryStatus() => canAct;

        public bool IsTargeted() => isTargeted;

        public EncounterAutoCharacterController GetTargeter() => targeter;
        
        #endregion
        
        #region Behaviour

        private void MoveToTarget()
        {
            
        }

        private void DoAction()
        {
            
        }

        private void MainBranch()
        {
            if (nothingToDo) return;

            if (target is not null) HasTargetBranch();
            else NoTargetBranch();
        }

        private void HasTargetBranch()  // A
        {
            if (queuedAction is not null) HasActionBranch();
            else NoActionBranch();
        }

        private void NoTargetBranch()  // B
        {
            if (targetOverflow) nothingToDo = true;
            else
            {
                ReTarget();
                if (target is null) targetOverflow = true;
            }
        }

        private void HasActionBranch()  // C
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < queuedActionRange) DoAction();
            else MoveToTarget();
        }

        private void NoActionBranch()  // D
        {
            if (actionOverflow) nothingToDo = true;
            else
            {
                QueueNewAction();
                if (queuedAction is null) actionOverflow = true;
            }
        }

        private void QueueNewAction()
        {
            // QueueNewAction() will find a new action to queue
            
            if (target is null) return;

            // Get cast ability
            if (QueueCastAbility()) return;

            // All abilities attached to items are put into the abilityShelf at initialization

            // attack
            if (QueueAttack()) return;
        }

        private bool QueueCastAbility()
        {
            RealAbilityData optimalAbility = null;
            CastUsagePreference leadingPref = CastUsagePreference.NONE;
            
            foreach (UnspecifiedAbilityMeta ability in abilityShelf.GetShelf())
            {
                if (!abilityShelf.AbilityIsOffCooldown(ability)) continue;
                
                optimalAbility = GetPreferableAbility(ability.GetRealData(), optimalAbility, leadingPref);
                leadingPref = optimalAbility.GetUsePreference();
            }

            if (optimalAbility is null) return false;
            
            queuedActionRange = optimalAbility.GetBaseData().GetRange();
            queuedAction = CastAbility;

            return true;

        }

        private RealAbilityData GetPreferableAbility(RealAbilityData ability, RealAbilityData selected, CastUsagePreference sPref)
        {
            if (selected is null) return ability;

            switch (sPref)
            {
                case CastUsagePreference.NONE:
                    return ability;
                case CastUsagePreference.Immediately:
                    return selected;
            }

            CastUsagePreference aPref = ability.GetUsePreference();
            
            switch (aPref)
            {
                case CastUsagePreference.Immediately:
                    return ability;
                case CastUsagePreference.Optimal:
                    return Random.value < 0.5f ? ability : selected;  // bug fix this!
                case CastUsagePreference.SelfLowHealth when EncounterManager.Instance.CharacterIsLowHealth(stats):
                    return ability;
                case CastUsagePreference.SelfMediumHealth when EncounterManager.Instance.CharacterIsMediumHealth(stats):
                    return ability;
                case CastUsagePreference.SelfHighHealth when EncounterManager.Instance.CharacterIsHighHealth(stats):
                    return ability;
                case CastUsagePreference.TargetLowHealth when EncounterManager.Instance.CharacterIsLowHealth(target.GetCharacterStatPacket()):
                    return ability;
                case CastUsagePreference.TargetMediumHealth when EncounterManager.Instance.CharacterIsMediumHealth(target.GetCharacterStatPacket()):
                    return ability;
                case CastUsagePreference.TargetHighHealth when EncounterManager.Instance.CharacterIsHighHealth(target.GetCharacterStatPacket()):
                    return ability;
                case CastUsagePreference.NONE:
                    return ability;
                default:
                    return selected;
            }
        }

        private bool QueueAttack()
        {
            queuedActionRange = character.GetCharacterData().GetAttackRange();
            queuedAction = Attack;

            return true;
        }
        
        #endregion
        
        #region Shelf Management

        private void OnUseAbility(RealAbilityData ability)
        {
            if (ability.IsAttachedToItem()) OnUseItem(ability.GetAttachmentItem());
            else
            {
                
            }
        }

        private void OnUseItem(RealItemData item)
        {
            itemShelf.OnUseItem(item);
        }
        
        #endregion
        
        #region Controls

        // Move is the only control called from a different script
        private void Move()
        {
            if (canMove) movement.DoCharacterMovement();
        }

        private bool Attack()
        {
            if (!canAttack) return false;
            
            return true;
        }

        private bool CastAbility()
        {
            if (!canCast) return false;
            
            return true;
        }

        private bool CastItem()
        {
            if (!canCast) return false;

            
            return true;
        }

        private bool ReTarget()
        {
            // target = EncounterManager.Instance.PerformReTargetAction(this, AffiliationManager.GetOpposingAffiliation(affiliation), target, GetPreferredReTargetMethod());
            
            return true;
        }
        
        #endregion
    }

    public struct CharacterControlActionPacket
    {
        
    }

    public enum CharacterControlAction
    {
        Move,  // vector3
        Attack,  // EncounterAutoCharacterController
        CastAbility,  // EncounterAutoCharacterController, BaseAbilityData
        CastItem,  // EncounterAutoCharacterController, BaseItemData
        ReTarget  //
    }

    public enum ReTargetMethod
    {
        Closest,
        MostHealth,
        MostMana,
        MostDangerous,
        MostAttack,
        NONE
    }

    public enum ReTargetPreference
    {
        Optimal,
        Always,
        HighHealth,
        MediumHealth,
        LowHealth,
        NONE
    }

    public enum CastUsagePreference
    {
        Optimal,
        Immediately,
        SelfLowHealth,
        SelfMediumHealth,
        SelfHighHealth,
        TargetLowHealth,
        TargetMediumHealth,
        TargetHighHealth,
        NONE
    }
}
