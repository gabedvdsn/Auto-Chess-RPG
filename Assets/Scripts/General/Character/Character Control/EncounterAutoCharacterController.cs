using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

namespace AutoChessRPG
{
    public class EncounterAutoCharacterController : ObservableSubject
    {
        // Encounter information
        private Affiliation affiliation;
        private EncounterAutoCharacterController target;

        private int identifier;
        
        // Individual information
        private Character character;

        private AbilityShelf abilityShelf;
        private ItemShelf itemShelf;
        
        private CharacterMovement movement;

        private CharacterEntityData data;
        private StatPacket stats;
        private EncounterPreferencesPacket preferences;
        
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

        private RealAbilityData queuedAbility;
        private RealItemData queuedItem;
        
        // Preferences information
        private ReTargetMethod alwaysReTargetMethod = ReTargetMethod.NONE;

        private void Update()
        {
            SendStatus();
            MainBranch();
        }

        public void Initialize(Affiliation _affiliation, EncounterPreferencesPacket _preferences, int _identifier)
        {
            character = GetComponent<Character>();
            data = character.GetCharacterData();

            character.Initialize();
            
            stats = character.GetCharacterStatPacket();
            preferences = _preferences;
            
            movement = GetComponent<CharacterMovement>();
            movement.Initialize(stats, 
                character.GetCharacterData().GetAllowableActionRange());
            
            affiliation = _affiliation;

            abilityShelf = character.GetCharacterAbilityShelf();
            itemShelf = character.GetCharacterItemShelf();
            
            InitialReTargetPreferenceCheck();

            nothingToDo = false;

            identifier = _identifier;
        }
        
        #region Statuses

        public void SendStatus()
        {
            float health = 1 - stats.currHealth / stats.maxHealth;
            float mana = 1 - stats.currMana / stats.maxMana;
            float support = (isTargeted ? 1 : 0) * ((health + mana) / 2);
            
        }

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

        private void ConvertControlActionToAction(CharacterControlAction action)
        {
            switch (action)
            {
                case CharacterControlAction.Attack:
                    queuedAction = Attack;
                    break;
                case CharacterControlAction.CastAbility:
                    queuedAction = CastAbility;
                    break;
                case CharacterControlAction.CastItem:
                    queuedAction = CastItem;
                    break;
                case CharacterControlAction.ReTarget:
                    ReTarget();
                    break;
                case CharacterControlAction.Move:
                    break;
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
            
            foreach (RealAbilityData ability in abilityShelf.GetShelf())
            {
                if (!abilityShelf.AbilityIsOffCooldown(ability)) continue;
                
                optimalAbility = GetPreferableAbility(ability, optimalAbility, leadingPref);
                leadingPref = preferences.GetAbilityUsagePreference(optimalAbility);
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

            CastUsagePreference aPref = preferences.GetAbilityUsagePreference(ability);
            
            switch (aPref)
            {
                case CastUsagePreference.Immediately:
                    return ability;
                case CastUsagePreference.Optimal when EncounterManager.Instance.AbilityIsMoreOptimalThan(ability, selected, target):
                    return ability;
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
        
        #region Checkers


        private bool IsLowHealth() => 0 < stats.currHealth && stats.currHealth <= stats.maxHealth * .2f;

        private bool IsMediumHealth() => stats.maxHealth * .2f < stats.currHealth && stats.currHealth <= stats.maxHealth * .6f;

        private bool IsHighHealth() => stats.maxHealth * .6f < stats.currHealth && stats.currHealth <= stats.maxHealth;
        
        #endregion
        
        #region Preferences Managing

        private void InitialReTargetPreferenceCheck()
        {
            foreach (ReTargetPreference reTargetPreference in preferences.retargetPreferences.Keys)
            {
                if (reTargetPreference != ReTargetPreference.Always) continue;
                
                alwaysReTargetMethod = preferences.retargetPreferences[reTargetPreference];
                return;
            }
        }

        private ReTargetMethod GetPreferredReTargetMethod()
        {
            if (alwaysReTargetMethod != ReTargetMethod.NONE) return alwaysReTargetMethod;

            ReTargetMethod choice = ReTargetMethod.NONE;
            
            foreach (ReTargetPreference reTargetPreference in preferences.retargetPreferences.Keys)
            {
                switch (reTargetPreference)
                {
                    case ReTargetPreference.Optimal:
                        return GetOptimalReTargetMethod();
                    case ReTargetPreference.HighHealth:
                        if (IsHighHealth()) choice = preferences.retargetPreferences[reTargetPreference];
                        break;
                    case ReTargetPreference.MediumHealth:
                        if (IsMediumHealth()) choice = preferences.retargetPreferences[reTargetPreference];
                        break;
                    case ReTargetPreference.LowHealth:
                        if (IsHighHealth()) choice = preferences.retargetPreferences[reTargetPreference];
                        break;
                }
            }

            return choice;
        }

        private ReTargetMethod GetOptimalReTargetMethod()
        {
            // If being targeted, get attacker
            if (isTargeted) return ReTargetMethod.MostDangerous;
            return ReTargetMethod.Closest;
        }
        
        #endregion
        
        #region Shelf Management

        private void UseAbility(BaseAbilityData baseAbility)
        {
            
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
