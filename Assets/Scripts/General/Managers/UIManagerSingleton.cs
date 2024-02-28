using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace AutoChessRPG
{
    public class UIManagerSingleton : MonoBehaviour
    {
        public static UIManagerSingleton Instance;

        [Header("Persistent UI Elements")] 
        [SerializeField] private Button settingsButton;

        [Header("Toggle UI Containers")]
        [SerializeField] private UIContainer defaultUIConfiguration;
        [SerializeField] private CharacterUIContainer characterUIConfiguration;
        [SerializeField] private BuildingUIContainer buildingUIConfiguration;

        private UIConfiguration currConfiguration;
        private UIContainer currContainer;

        private bool showUI;

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
        
        public void SetShowUI(bool value) => showUI = value;

        public void OnClickDefault()
        {
            
        }

        public void OnClickCharacter()
        {
            ChangeUIConfiguration(UIConfiguration.Character);
        }

        public void OnClickBuilding()
        {
            
        }

        private void ChangeUIConfiguration(UIConfiguration configuration)
        {
            if (!showUI) return;
            if (configuration == currConfiguration) return;
            
            switch (configuration)
            {
                case UIConfiguration.Default:
                    ToggleUIConfiguration(configuration, defaultUIConfiguration);
                    break;
                case UIConfiguration.Character:
                    ToggleUIConfiguration(configuration, characterUIConfiguration);
                    break;
                case UIConfiguration.Building:
                    ToggleUIConfiguration(configuration, buildingUIConfiguration);
                    break;
                case UIConfiguration.NONE:
                    ToggleUIConfiguration(configuration, null);
                    break;
                default:
                    ToggleUIConfiguration(UIConfiguration.Default, defaultUIConfiguration);
                    break;
            }
            
        }

        private void ToggleUIConfiguration(UIConfiguration configuration, UIContainer container)
        {
            // update currConfiguration
            currConfiguration = configuration;

            // if currContainer is not null, toggle it off
            if (currContainer is not null) currContainer.ToggleActiveState();
            
            // update currContainer
            currContainer = container;

            // if container is null (NONE configuration) then return
            if (currContainer is null) return;
            
            // if currContainer is not null, toggle it on
            currContainer.ToggleActiveState();
        }
    }

    public enum UIConfiguration
    {
        Default,
        Character,
        Building,
        NONE
    }
}
