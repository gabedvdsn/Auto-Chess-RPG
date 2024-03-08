using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutoChessRPG
{
    public class CharacterPreviewUIContainer : UIContainer
    {
        [SerializeField] private Character target;
        
        [Header("Entity Displays")]
        [SerializeField] private TMP_Text nameText;
        
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

        public void SendSelectedCharacter(Character character)
        {
            selectedCharacter = character;
            stats = character.GetCharacterStatPacket();
            attributes = character.GetCharacterAttributePacket();
            
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

            attackDamageText.text = stats.attackDamage.ToString(CultureInfo.InvariantCulture);
            attackSpeedText.text = (stats.attackSpeed * StatsParameters.REAL_ATTACK_SPEED_TO_GAME).ToString(CultureInfo.InvariantCulture);
            moveSpeedText.text = (stats.moveSpeed * StatsParameters.REAL_MOVESPEED_TO_GAME).ToString(CultureInfo.InvariantCulture);
            armorText.text = stats.armor.ToString(CultureInfo.InvariantCulture);
            negationText.text = stats.negation.ToString(CultureInfo.InvariantCulture);
            debuffResistText.text = (stats.debuffResistance * StatsParameters.REAL_PERCENT_TO_GAME).ToString(CultureInfo.InvariantCulture);
        }

    }
}
