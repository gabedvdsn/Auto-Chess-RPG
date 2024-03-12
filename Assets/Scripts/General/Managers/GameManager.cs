using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static Character Hero;
        
        [SerializeField] private GameObject encounterManagerPrefab;
        
        private GameMode gamemode;

        private bool playerClickToMove;  // player can click on ground to move freely
        private bool playerHasControl;  // player can use items and abilities

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                EnterEncounter();
            }
        }

        public void EnterEncounter()
        {
            SwitchGameMode(GameMode.Encounter);

            OnEnterEncounter();
        }
        
        private void OnEnterEncounter()
        {

        }

        public void ExitEncounter()
        {
            
        }

        private void InitializeEncounterManager()
        {
            
        }

        public void EnterWorldSpace()
        {
            
        }

        public void EnterMysteryArea()
        {
            
        }

        public void EnterRaid()
        {
            
        }

        private void SwitchGameMode(GameMode mode)
        {
            if (mode == gamemode) return;
            
            gamemode = mode;

            switch (gamemode)
            {

                case GameMode.SingleControl:
                    break;
                case GameMode.FollowControl:
                    break;
                case GameMode.Encounter:
                    break;
                case GameMode.Cutscene:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool AssignHero(Character hero)
        {
            if (Hero != null) return false;

            Hero = hero;

            return true;
        }
    }

    public enum GameMode
    {
        SingleControl,  // gives player click-to-move control (solo)
        FollowControl,  // gives player click-to-move control (minions follow)
        Encounter,  // relinquishes player click-to-move control, auto battle
        Cutscene  // relinquishes player control
    }
}
