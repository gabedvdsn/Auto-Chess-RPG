using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using UnityEngine;
using UnityEngine.UIElements;

namespace AutoChessRPG
{
    public class EncounterAutoCharacterController : ObservableSubject
    {
        // Encounter information
        private Affiliation affiliation;
        
        // Individual information
        private Character character;
        private CharacterMovement movement;

        private CharacterEntityData data;
        private StatPacket stats;
        private EncounterPreferencesPacket preferences;

        private EncounterAutoCharacterController target;

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

        private bool nothingToDo;
        private bool actionOverflow;
        private bool targetOverflow;
        
        private bool isTargeted;

        private BaseAbilityData _queuedBaseAbility;
        private BaseItemData _queuedBaseItem;
        
        // Preferences information
        private ReTargetMethod alwaysReTargetMethod = ReTargetMethod.NONE;
        
        private void Awake()
        {
            
        }

        private void Update()
        {
            Move();
        }

        public void Initialize(Affiliation _affiliation, EncounterPreferencesPacket _preferences)
        {
            character = GetComponent<Character>();
            data = character.GetCharacterData();
            
            movement = GetComponentInChildren<CharacterMovement>();
            movement.Initialize(character.GetCharacterData().GetMovementSpeed(), 
                character.GetCharacterData().GetRotationSpeed(), 
                character.GetCharacterData().GetAllowableActionRange());
            
            stats = character.GetCharacterStatPacket();
            preferences = _preferences;

            affiliation = _affiliation;
            
            InitialReTargetPreferenceCheck();
        }
        
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

            if (target is not null) BranchA();
            else BranchB();
        }

        private void BranchA()
        {
            if (queuedAction is not null) BranchC();
            else BranchD();
        }

        private void BranchB()
        {
            if (targetOverflow) nothingToDo = true;
            else
            {
                ReTarget();
                if (target is null) targetOverflow = true;
            }
        }

        private void BranchC()
        {
            if (Vector3.Distance(transform.position, target.transform.position) < queuedActionRange) DoAction();
            else MoveToTarget();
        }

        private void BranchD()
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

            // 
        }

        private BaseAbilityData QueueCastAbility()
        {
            return default;
        }

        private BaseItemData QueueCastItem()
        {
            return default;
        }

        private bool QueueAttack()
        {
            return default;
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
            target = EncounterManager.Instance.PerformReTargetAction(this, AffiliationManager.GetOpposingAffiliation(character.GetAffiliation()), target, GetPreferredReTargetMethod());
            
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
        LowHealth
    }

    public enum CastUsagePreference
    {
        Optimal,
        Immediately,
        LowHealth,
        MediumHealth
    }
}
