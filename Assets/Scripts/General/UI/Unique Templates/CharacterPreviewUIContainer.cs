using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AutoChessRPG
{
    public class CharacterPreviewUIContainer : UIContainer
    {
        [SerializeField] private Character target;
        
        [Header("Entity Displays")]
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private RawImage displayImage;
        
        [Space]
        
        [Header("Attribute Displays")]
        
        [SerializeField] private TMP_Text strengthText;
        [SerializeField] private TMP_Text agilityText;
        [SerializeField] private TMP_Text intelligenceText;
        
        [Space]
        
        [Header("Stat Displays")]
        
        [SerializeField] private TMP_Text attackDamageText;
        [SerializeField] private TMP_Text attackSpeedText;
        [SerializeField] private TMP_Text moveSpeedText;
        [SerializeField] private TMP_Text armorText;
        [SerializeField] private TMP_Text negationText;
        [SerializeField] private TMP_Text debuffResistText;

        private Character selectedCharacter;
        private StatPacket stats;
        private RealAttributePacket attributes;

        private Camera portraitCam;

        public void SendSelectedCharacter(Character character)
        {
            selectedCharacter = character;
            stats = character.GetCharacterStatPacket();
            attributes = character.GetCharacterAttributePacket();

            // portraitCam = character.gameObject.GetComponentInChildren<Camera>();

            UpdateConcreteUIElements();
        }

        private void Start()
        {
            SendSelectedCharacter(target);
        }

        private void Update()
        {
            UpdateDynamicUIElements();
        }

        private void UpdateConcreteUIElements()
        {
            nameText.text = selectedCharacter.GetBaseData().GetEntityName();
        }

        private void UpdateDynamicUIElements()
        {
            strengthText.text = attributes.Strength().ToString();
            agilityText.text = attributes.Agility().ToString();
            intelligenceText.text = attributes.Intelligence().ToString();

            attackDamageText.text = Mathf.RoundToInt(stats.attackDamage).ToString(CultureInfo.InvariantCulture);
            attackSpeedText.text = Mathf.RoundToInt(stats.attackSpeed * StatsParameters.REAL_ATTACK_SPEED_TO_GAME).ToString(CultureInfo.InvariantCulture);
            moveSpeedText.text = Mathf.RoundToInt(stats.moveSpeed * StatsParameters.REAL_MOVESPEED_TO_GAME).ToString(CultureInfo.InvariantCulture);
            armorText.text = Mathf.RoundToInt(stats.armor).ToString(CultureInfo.InvariantCulture);
            negationText.text = Mathf.RoundToInt(stats.negation).ToString(CultureInfo.InvariantCulture);
            debuffResistText.text = Mathf.RoundToInt(stats.debuffResistance * StatsParameters.REAL_PERCENT_TO_GAME).ToString(CultureInfo.InvariantCulture) + "%";
        }

    }
}
