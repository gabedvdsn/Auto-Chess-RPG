using System;
using AutoChessRPG.Entity.Character;
using UnityEngine;

namespace AutoChessRPG
{
    public class CharacterEntityController : ObservableSubject
    {
        private Character character;
        private CharacterMovement movement;

        private CharacterEntityData data;

        private CharacterEntityController target;

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

        private void Awake()
        {
            character = GetComponent<Character>();
            movement = GetComponentInChildren<CharacterMovement>();

            data = character.GetCharacterData();
        }

        private void Update()
        {
            Move();
        }
        
        #region Statuses

        public void SetPrimaryStatus(bool status) => canAct = status;

        public void SetDead(bool status = true) => isDead = status;
        
        private bool CharacterMayTarget() => movement.GetDegreesToTarget() < data.GetAllowableActionRange();
        
        #endregion
        
        #region Getters

        public Character GetCharacter() => character;

        public StatPacket GetCharacterStatPacket() => character.getCharacterStatPacket();

        public CharacterMovement GetCharacterMovement() => movement;
        
        public CharacterEntityController GetTarget() => target;

        public CharacterEntityData GetCharacterData() => data;

        public bool GetPrimaryStatus() => canAct;
        
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

        private void ReTarget(ReTargetMethod reTargetMethod)
        {
            target = EncounterManagerSingleton.Instance.PerformReTargetAction(this, target, reTargetMethod);
            
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
        Attack,  // CharacterEntityController
        CastAbility,  // CharacterEntityController, AbilityData
        CastItem,  // CharacterEntityController, ItemData
        ReTarget  //
    }

    public enum ReTargetMethod
    {
        Closest,
        MostHealth,
        MostMana,
        MostDangerous,
        MostAttack
    }
}
