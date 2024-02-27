using System;
using System.Collections;
using System.Collections.Generic;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class EncounterAutoCharacterController : ObservableSubject
    {
        // Individual information
        private Character character;
        private CharacterMovement movement;

        private CharacterEntityData data;
        private StatPacket stats;
        private EncounterPreferencesPacket preferences;

        private EncounterAutoCharacterController enemyTarget;
        private EncounterAutoCharacterController allyTarget;

        // Status information
        private bool isDead
        {
            set => canAct = !value;
            get => isDead;
        }

        private bool canAttack;
        private bool canCast;
        private bool canMove;

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
        private delegate bool CurrActionDelegate();
        private CurrActionDelegate currAction;

        private bool nothingToDo;
        private bool movingToAction;
        private bool isTargeted;

        private AbilityData queuedAbility;
        private ItemData queuedItem;
        
        // Preferences information
        private ReTargetMethod alwaysReTargetMethod = ReTargetMethod.NONE;
        
        private void Awake()
        {
            
        }

        private void Update()
        {
            Move();
        }

        public void Initialize(EncounterPreferencesPacket _preferences)
        {
            character = GetComponent<Character>();
            movement = GetComponentInChildren<CharacterMovement>();

            data = character.GetCharacterData();
            stats = character.GetCharacterStatPacket();
            preferences = _preferences;
            
            InitialReTargetPreferenceCheck();
        }
        
        #region Statuses

        public void SetPrimaryStatus(bool status) => canAct = status;

        public void SetDead(bool status = true) => isDead = status;
        
        private bool CharacterMayTarget() => movement.GetDegreesToTarget() < data.GetAllowableActionRange();
        
        #endregion
        
        #region Getters

        public Character GetCharacter() => character;

        public StatPacket GetCharacterStatPacket() => character.GetCharacterStatPacket();

        public CharacterMovement GetCharacterMovement() => movement;
        
        public EncounterAutoCharacterController GetTarget() => enemyTarget;

        public CharacterEntityData GetCharacterData() => data;

        public bool GetPrimaryStatus() => canAct;
        
        #endregion
        
        #region Controls Readers

        private void ConvertControlActionToAction(CharacterControlAction action)
        {
            switch (action)
            {
                case CharacterControlAction.Attack:
                    currAction = Attack;
                    break;
                case CharacterControlAction.CastAbility:
                    currAction = CastAbility;
                    break;
                case CharacterControlAction.CastItem:
                    currAction = CastItem;
                    break;
                case CharacterControlAction.ReTarget:
                    ReTarget(preferences.reTargetMethod);
                    break;
                case CharacterControlAction.Move:
                    break;
            }
        }

        private void GetNewAction()
        {
            // FindNewAction() will find a new action to add to queue
            
            // retarget
            if (enemyTarget is null)
            {
                ReTarget();
            }
            
            //
        }

        private AbilityData CheckForCastAbility()
        {
            
        }

        private ItemData CheckForCastItem()
        {
            
        }

        private bool CheckForAttack()
        {
            
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

        private void UseAbility(AbilityData ability, Character)
        {
            if (ability.GetAbilityActivation() == )
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

            character.UseAbility(queuedAbility);
            
            return true;
        }

        private bool ReTarget(ReTargetMethod reTargetMethod)
        {
            enemyTarget = EncounterManagerSingleton.Instance.PerformReTargetAction(this, enemyTarget, GetPreferredReTargetMethod());
            
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
        CastAbility,  // EncounterAutoCharacterController, AbilityData
        CastItem,  // EncounterAutoCharacterController, ItemData
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
