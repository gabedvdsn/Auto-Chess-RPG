using System;
using UnityEngine;

namespace AutoChessRPG
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject encounterManagerPrefab;
        
        [SerializeField] private Character[] allyCharacters;
        [SerializeField] private Character[] enemyCharacters;

        private GameMode gamemode;

        private bool playerClickToMove;  // player can click on ground to move freely
        private bool playerHasControl;  // player can use items and abilities

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
            // Instantiate encounterManagerPrefab as child
            foreach (Character ally in allyCharacters)
            {
                ally.Initialize(new EncounterPreferencesPacket());
            }

            foreach (Character enemy in enemyCharacters)
            {
                enemy.Initialize(new EncounterPreferencesPacket());
            }
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

        public void EnterExplorationZone()
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
    }

    public enum GameMode
    {
        SingleControl,  // gives player click-to-move control (solo)
        FollowControl,  // gives player click-to-move control (minions follow)
        Encounter,  // relinquishes player click-to-move control, auto battle
        Cutscene  // relinquishes player control
    }
}
